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
    public class CompanyPositionReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[CompanyPosition] " +
                            "([CompanyPositionPK],[HistoryPK],[Status],[ID],[Name],[Groups],[Levels],[ParentPK]," +
                            "[ParentPK1],[ParentPK2],[ParentPK3],[ParentPK4],[ParentPK5],[ParentPK6],[ParentPK7],[ParentPK8],[ParentPK9],";
        string _paramaterCommand = "@ID,@Name,@Groups,@Levels,@ParentPK,@ParentPK1,@ParentPK2,@ParentPK3,@ParentPK4,@ParentPK5,@ParentPK6,@ParentPK7,@ParentPK8,@ParentPK9,";

        //2
        private CompanyPosition setCompanyPosition(SqlDataReader dr)
        {
            CompanyPosition M_CompanyPosition = new CompanyPosition();
            M_CompanyPosition.CompanyPositionPK = Convert.ToInt32(dr["CompanyPositionPK"]);
            M_CompanyPosition.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_CompanyPosition.Status = Convert.ToInt32(dr["Status"]);
            M_CompanyPosition.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_CompanyPosition.Notes = Convert.ToString(dr["Notes"]);
            M_CompanyPosition.ID = Convert.ToString(dr["ID"]);
            M_CompanyPosition.Name = Convert.ToString(dr["Name"]);
            M_CompanyPosition.Groups = Convert.ToBoolean(dr["Groups"]);
            M_CompanyPosition.Levels = Convert.ToInt32(dr["Levels"]);
            M_CompanyPosition.ParentPK = Convert.ToInt32(dr["ParentPK"]);
            M_CompanyPosition.ParentID = dr["ParentID"].ToString();
            M_CompanyPosition.ParentName = dr["ParentName"].ToString();
            M_CompanyPosition.ParentPK1 = Convert.ToInt32(dr["ParentPK1"]);
            M_CompanyPosition.ParentPK2 = Convert.ToInt32(dr["ParentPK2"]);
            M_CompanyPosition.ParentPK3 = Convert.ToInt32(dr["ParentPK3"]);
            M_CompanyPosition.ParentPK4 = Convert.ToInt32(dr["ParentPK4"]);
            M_CompanyPosition.ParentPK5 = Convert.ToInt32(dr["ParentPK5"]);
            M_CompanyPosition.ParentPK6 = Convert.ToInt32(dr["ParentPK6"]);
            M_CompanyPosition.ParentPK7 = Convert.ToInt32(dr["ParentPK7"]);
            M_CompanyPosition.ParentPK8 = Convert.ToInt32(dr["ParentPK8"]);
            M_CompanyPosition.ParentPK9 = Convert.ToInt32(dr["ParentPK9"]);
            M_CompanyPosition.Depth = Convert.ToInt32(dr["Depth"]);
            M_CompanyPosition.EntryUsersID = dr["EntryUsersID"].ToString();
            M_CompanyPosition.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_CompanyPosition.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_CompanyPosition.VoidUsersID = dr["VoidUsersID"].ToString();
            M_CompanyPosition.EntryTime = dr["EntryTime"].ToString();
            M_CompanyPosition.UpdateTime = dr["UpdateTime"].ToString();
            M_CompanyPosition.ApprovedTime = dr["ApprovedTime"].ToString();
            M_CompanyPosition.VoidTime = dr["VoidTime"].ToString();
            M_CompanyPosition.DBUserID = dr["DBUserID"].ToString();
            M_CompanyPosition.DBTerminalID = dr["DBTerminalID"].ToString();
            M_CompanyPosition.LastUpdate = dr["LastUpdate"].ToString();
            M_CompanyPosition.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_CompanyPosition;
        }

        public List<CompanyPosition> CompanyPosition_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    List<CompanyPosition> L_CompanyPosition = new List<CompanyPosition>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            ZA.ID ParentID,ZA.Name ParentName,
                            A.* from CompanyPosition A 
                            Left join CompanyPosition ZA on A.ParentPK = ZA.CompanyPositionPK and ZA.status in (1,2) 
                            where A.status = @status order by A.CompanyPositionPK ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            ZA.ID ParentID,ZA.Name ParentName,
                            A.* from CompanyPosition A 
                            Left join CompanyPosition ZA on A.ParentPK = ZA.CompanyPositionPK and ZA.status in (1,2) 
                            order by A.CompanyPositionPK  ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_CompanyPosition.Add(setCompanyPosition(dr));
                                }
                            }
                            return L_CompanyPosition;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int CompanyPosition_Add(CompanyPosition _CompanyPosition, bool _havePrivillege)
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
                                 "Select isnull(max(CompanyPositionPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from CompanyPosition";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _CompanyPosition.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                      
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(CompanyPositionPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from CompanyPosition";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _CompanyPosition.ID);
                        cmd.Parameters.AddWithValue("@Name", _CompanyPosition.Name);
                        cmd.Parameters.AddWithValue("@Groups", _CompanyPosition.Groups);
                        cmd.Parameters.AddWithValue("@Levels", _CompanyPosition.Levels);
                        cmd.Parameters.AddWithValue("@ParentPK", _CompanyPosition.ParentPK);
                        cmd.Parameters.AddWithValue("@ParentPK1", _CompanyPosition.ParentPK1);
                        cmd.Parameters.AddWithValue("@ParentPK2", _CompanyPosition.ParentPK2);
                        cmd.Parameters.AddWithValue("@ParentPK3", _CompanyPosition.ParentPK3);
                        cmd.Parameters.AddWithValue("@ParentPK4", _CompanyPosition.ParentPK4);
                        cmd.Parameters.AddWithValue("@ParentPK5", _CompanyPosition.ParentPK5);
                        cmd.Parameters.AddWithValue("@ParentPK6", _CompanyPosition.ParentPK6);
                        cmd.Parameters.AddWithValue("@ParentPK7", _CompanyPosition.ParentPK7);
                        cmd.Parameters.AddWithValue("@ParentPK8", _CompanyPosition.ParentPK8);
                        cmd.Parameters.AddWithValue("@ParentPK9", _CompanyPosition.ParentPK9);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _CompanyPosition.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "CompanyPosition");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int CompanyPosition_Update(CompanyPosition _CompanyPosition, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_CompanyPosition.CompanyPositionPK, _CompanyPosition.HistoryPK, "CompanyPosition");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CompanyPosition set status=2, Notes=@Notes,ID=@ID,Name=@Name, " +
                                "Groups=@Groups,Levels=@Levels,ParentPK=@ParentPK,ParentPK1=@ParentPK1,ParentPK2=@ParentPK2,ParentPK3=@ParentPK3,ParentPK4=@ParentPK4," +
                                "ParentPK5=@ParentPK5,ParentPK6=@ParentPK6,ParentPK7=@ParentPK7,ParentPK8=@ParentPK8,ParentPK9=@ParentPK9," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastUpdate " +
                                "where CompanyPositionPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _CompanyPosition.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _CompanyPosition.CompanyPositionPK);
                            cmd.Parameters.AddWithValue("@Notes", _CompanyPosition.Notes);
                            cmd.Parameters.AddWithValue("@ID", _CompanyPosition.ID);
                            cmd.Parameters.AddWithValue("@Name", _CompanyPosition.Name);
                            cmd.Parameters.AddWithValue("@Groups", _CompanyPosition.Groups);
                            cmd.Parameters.AddWithValue("@Levels", _CompanyPosition.Levels);
                            cmd.Parameters.AddWithValue("@ParentPK", _CompanyPosition.ParentPK);
                            cmd.Parameters.AddWithValue("@ParentPK1", _CompanyPosition.ParentPK1);
                            cmd.Parameters.AddWithValue("@ParentPK2", _CompanyPosition.ParentPK2);
                            cmd.Parameters.AddWithValue("@ParentPK3", _CompanyPosition.ParentPK3);
                            cmd.Parameters.AddWithValue("@ParentPK4", _CompanyPosition.ParentPK4);
                            cmd.Parameters.AddWithValue("@ParentPK5", _CompanyPosition.ParentPK5);
                            cmd.Parameters.AddWithValue("@ParentPK6", _CompanyPosition.ParentPK6);
                            cmd.Parameters.AddWithValue("@ParentPK7", _CompanyPosition.ParentPK7);
                            cmd.Parameters.AddWithValue("@ParentPK8", _CompanyPosition.ParentPK8);
                            cmd.Parameters.AddWithValue("@ParentPK9", _CompanyPosition.ParentPK9);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _CompanyPosition.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _CompanyPosition.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CompanyPosition set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CompanyPositionPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _CompanyPosition.CompanyPositionPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _CompanyPosition.EntryUsersID);
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
                                cmd.CommandText = "Update CompanyPosition set  Notes=@Notes,ID=@ID,Name=@Name," +
                                "Groups=@Groups,Levels=@Levels,ParentPK=@ParentPK,ParentPK1=@ParentPK1,ParentPK2=@ParentPK2,ParentPK3=@ParentPK3,ParentPK4=@ParentPK4," +
                                "ParentPK5=@ParentPK5,ParentPK6=@ParentPK6,ParentPK7=@ParentPK7,ParentPK8=@ParentPK8,ParentPK9=@ParentPK9," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where CompanyPositionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _CompanyPosition.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _CompanyPosition.CompanyPositionPK);
                                cmd.Parameters.AddWithValue("@Notes", _CompanyPosition.Notes);
                                cmd.Parameters.AddWithValue("@ID", _CompanyPosition.ID);
                                cmd.Parameters.AddWithValue("@Name", _CompanyPosition.Name);
                                cmd.Parameters.AddWithValue("@Groups", _CompanyPosition.Groups);
                                cmd.Parameters.AddWithValue("@Levels", _CompanyPosition.Levels);
                                cmd.Parameters.AddWithValue("@ParentPK", _CompanyPosition.ParentPK);
                                cmd.Parameters.AddWithValue("@ParentPK1", _CompanyPosition.ParentPK1);
                                cmd.Parameters.AddWithValue("@ParentPK2", _CompanyPosition.ParentPK2);
                                cmd.Parameters.AddWithValue("@ParentPK3", _CompanyPosition.ParentPK3);
                                cmd.Parameters.AddWithValue("@ParentPK4", _CompanyPosition.ParentPK4);
                                cmd.Parameters.AddWithValue("@ParentPK5", _CompanyPosition.ParentPK5);
                                cmd.Parameters.AddWithValue("@ParentPK6", _CompanyPosition.ParentPK6);
                                cmd.Parameters.AddWithValue("@ParentPK7", _CompanyPosition.ParentPK7);
                                cmd.Parameters.AddWithValue("@ParentPK8", _CompanyPosition.ParentPK8);
                                cmd.Parameters.AddWithValue("@ParentPK9", _CompanyPosition.ParentPK9);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _CompanyPosition.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_CompanyPosition.CompanyPositionPK, "CompanyPosition");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From CompanyPosition where CompanyPositionPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _CompanyPosition.CompanyPositionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _CompanyPosition.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _CompanyPosition.ID);
                                cmd.Parameters.AddWithValue("@Name", _CompanyPosition.Name);
                                cmd.Parameters.AddWithValue("@Groups", _CompanyPosition.Groups);
                                cmd.Parameters.AddWithValue("@Levels", _CompanyPosition.Levels);
                                cmd.Parameters.AddWithValue("@ParentPK", _CompanyPosition.ParentPK);
                                cmd.Parameters.AddWithValue("@ParentPK1", _CompanyPosition.ParentPK1);
                                cmd.Parameters.AddWithValue("@ParentPK2", _CompanyPosition.ParentPK2);
                                cmd.Parameters.AddWithValue("@ParentPK3", _CompanyPosition.ParentPK3);
                                cmd.Parameters.AddWithValue("@ParentPK4", _CompanyPosition.ParentPK4);
                                cmd.Parameters.AddWithValue("@ParentPK5", _CompanyPosition.ParentPK5);
                                cmd.Parameters.AddWithValue("@ParentPK6", _CompanyPosition.ParentPK6);
                                cmd.Parameters.AddWithValue("@ParentPK7", _CompanyPosition.ParentPK7);
                                cmd.Parameters.AddWithValue("@ParentPK8", _CompanyPosition.ParentPK8);
                                cmd.Parameters.AddWithValue("@ParentPK9", _CompanyPosition.ParentPK9);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _CompanyPosition.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update CompanyPosition set status= 4,Notes=@Notes, " +
                                " lastupdate=@lastupdate where CompanyPositionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _CompanyPosition.Notes);
                                cmd.Parameters.AddWithValue("@PK", _CompanyPosition.CompanyPositionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _CompanyPosition.HistoryPK);
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

        public void CompanyPosition_Approved(CompanyPosition _CompanyPosition)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CompanyPosition set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where CompanyPositionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CompanyPosition.CompanyPositionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CompanyPosition.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _CompanyPosition.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CompanyPosition set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CompanyPositionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _CompanyPosition.CompanyPositionPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CompanyPosition.ApprovedUsersID);
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

        public void CompanyPosition_Reject(CompanyPosition _CompanyPosition)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CompanyPosition set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where CompanyPositionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CompanyPosition.CompanyPositionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CompanyPosition.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CompanyPosition.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CompanyPosition set status= 2,LastUpdate=@LastUpdate where CompanyPositionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _CompanyPosition.CompanyPositionPK);
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

        public void CompanyPosition_Void(CompanyPosition _CompanyPosition)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CompanyPosition set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@lastUpdate " +
                            "where CompanyPositionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CompanyPosition.CompanyPositionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CompanyPosition.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CompanyPosition.VoidUsersID);
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

        public List<CompanyPositionCombo> CompanyPosition_ComboRpt()
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CompanyPositionCombo> L_CompanyPosition = new List<CompanyPositionCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "SELECT  CompanyPositionPK,ID + ' - ' + Name as ID, Name FROM [CompanyPosition]  where status = 2 union all select 0,'All', '' order by CompanyPositionPK,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CompanyPositionCombo M_CompanyPosition = new CompanyPositionCombo();
                                    M_CompanyPosition.CompanyPositionPK = Convert.ToInt32(dr["CompanyPositionPK"]);
                                    M_CompanyPosition.ID = Convert.ToString(dr["ID"]);
                                    M_CompanyPosition.Name = Convert.ToString(dr["Name"]);
                                    L_CompanyPosition.Add(M_CompanyPosition);
                                }

                            }
                        }
                        return L_CompanyPosition;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }

        public List<CompanyPositionCombo> CompanyPosition_Combo()
        {
   
             try
            {
                
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CompanyPositionCombo> L_CompanyPosition = new List<CompanyPositionCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
               
              
                    cmd.CommandText = "SELECT  CompanyPositionPK,ID + ' - ' + Name ID, Name FROM [CompanyPosition]  where status = 2 order by ID,Name";
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                CompanyPositionCombo M_CompanyPosition = new CompanyPositionCombo();
                                M_CompanyPosition.CompanyPositionPK = Convert.ToInt32(dr["CompanyPositionPK"]);
                                M_CompanyPosition.ID = Convert.ToString(dr["ID"]);
                                M_CompanyPosition.Name = Convert.ToString(dr["Name"]);
                                L_CompanyPosition.Add(M_CompanyPosition);
                            }

                        }
                    }
                        return L_CompanyPosition;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            

        }


        public List<CompanyPositionCombo> CompanyPosition_ComboGroupOnly()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CompanyPositionCombo> L_CompanyPosition = new List<CompanyPositionCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  CompanyPositionPK,ID + ' - ' + Name as [ID], Name FROM [CompanyPosition]  where Groups = 1 and status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CompanyPositionCombo M_CompanyPosition = new CompanyPositionCombo();
                                    M_CompanyPosition.CompanyPositionPK = Convert.ToInt32(dr["CompanyPositionPK"]);
                                    M_CompanyPosition.ID = Convert.ToString(dr["ID"]);
                                    M_CompanyPosition.Name = Convert.ToString(dr["Name"]);
                                    L_CompanyPosition.Add(M_CompanyPosition);
                                }
                            }
                            return L_CompanyPosition;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<CompanyPositionCombo> CompanyPosition_ComboChildOnly()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CompanyPositionCombo> L_CompanyPosition = new List<CompanyPositionCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  CompanyPositionPK,ID + ' - ' + Name as [ID], Name FROM [CompanyPosition]  where Groups = 0 and status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CompanyPositionCombo M_CompanyPosition = new CompanyPositionCombo();
                                    M_CompanyPosition.CompanyPositionPK = Convert.ToInt32(dr["CompanyPositionPK"]);
                                    M_CompanyPosition.ID = Convert.ToString(dr["ID"]);
                                    M_CompanyPosition.Name = Convert.ToString(dr["Name"]);
                                    L_CompanyPosition.Add(M_CompanyPosition);
                                }
                            }
                            return L_CompanyPosition;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public bool CompanyPosition_UpdateParentAndDepth()
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE CompanyPosition SET " +
                                                "CompanyPosition.ParentPK1 = isnull(CompanyPosition_1.CompanyPositionPK,0), CompanyPosition.ParentPK2 = isnull(CompanyPosition_2.CompanyPositionPK,0), " +
                                                "CompanyPosition.ParentPK3 = isnull(CompanyPosition_3.CompanyPositionPK,0), CompanyPosition.ParentPK4 = isnull(CompanyPosition_4.CompanyPositionPK,0), " +
                                                "CompanyPosition.ParentPK5 = isnull(CompanyPosition_5.CompanyPositionPK,0), CompanyPosition.ParentPK6 = isnull(CompanyPosition_6.CompanyPositionPK,0), " +
                                                "CompanyPosition.ParentPK7 = isnull(CompanyPosition_7.CompanyPositionPK,0), CompanyPosition.ParentPK8 = isnull(CompanyPosition_8.CompanyPositionPK,0), " +
                                                "CompanyPosition.ParentPK9 = isnull(CompanyPosition_9.CompanyPositionPK,0)  " +
                                                "FROM CompanyPosition " +
                                                "LEFT JOIN CompanyPosition AS CompanyPosition_1 ON CompanyPosition.ParentPK = CompanyPosition_1.CompanyPositionPK " +
                                                "LEFT JOIN CompanyPosition AS CompanyPosition_2 ON CompanyPosition_1.ParentPK = CompanyPosition_2.CompanyPositionPK " +
                                                "LEFT JOIN CompanyPosition AS CompanyPosition_3 ON CompanyPosition_2.ParentPK = CompanyPosition_3.CompanyPositionPK " +
                                                "LEFT JOIN CompanyPosition AS CompanyPosition_4 ON CompanyPosition_3.ParentPK = CompanyPosition_4.CompanyPositionPK " +
                                                "LEFT JOIN CompanyPosition AS CompanyPosition_5 ON CompanyPosition_4.ParentPK = CompanyPosition_5.CompanyPositionPK " +
                                                "LEFT JOIN CompanyPosition AS CompanyPosition_6 ON CompanyPosition_5.ParentPK = CompanyPosition_6.CompanyPositionPK " +
                                                "LEFT JOIN CompanyPosition AS CompanyPosition_7 ON CompanyPosition_6.ParentPK = CompanyPosition_7.CompanyPositionPK " +
                                                "LEFT JOIN CompanyPosition AS CompanyPosition_8 ON CompanyPosition_7.ParentPK = CompanyPosition_8.CompanyPositionPK " +
                                                "LEFT JOIN CompanyPosition AS CompanyPosition_9 ON CompanyPosition_8.ParentPK = CompanyPosition_9.CompanyPositionPK Where CompanyPosition.Status = 2 ";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Select CompanyPositionPK From CompanyPosition Where Status = 2 and Groups = 1";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                try
                                {
                                    using (SqlConnection DbConSubQuery = new SqlConnection(Tools.conString))
                                    {
                                        DbConSubQuery.Open();
                                        while (dr.Read())
                                        {
                                            using (SqlCommand cmdSubQuery = DbConSubQuery.CreateCommand())
                                            {
                                                cmdSubQuery.CommandText = "Update CompanyPosition set Depth = @Depth, lastupdate=@lastupdate Where CompanyPositionPK = @CompanyPositionPK and Status = 2";
                                                cmdSubQuery.Parameters.AddWithValue("@Depth", GetCompanyPositionDepth(Convert.ToInt32(dr["CompanyPositionPK"])));
                                                cmdSubQuery.Parameters.AddWithValue("@CompanyPositionPK", Convert.ToInt32(dr["CompanyPositionPK"]));
                                                cmdSubQuery.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                                cmdSubQuery.ExecuteNonQuery();
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
                        return true;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private int GetCompanyPositionDepth(int _CompanyPositionPK)
        {
            try
            {
                using (SqlConnection DbConSubQuery = new SqlConnection(Tools.conString))
                {
                    DbConSubQuery.Open();
                    using (SqlCommand cmd = DbConSubQuery.CreateCommand())
                    {
                        cmd.CommandText = "DECLARE @Depth INT,@Depth1 INT, @Depth2 INT, @Depth3 INT, @Depth4 INT, @Depth5 INT, " +
                                          "@Depth6 INT, @Depth7 INT, @Depth8 INT, @Depth9 INT, @Depth10 INT " +
                                          "SELECT @Depth1 = MAX(CompanyPosition_1.ParentPK), @Depth2 = MAX(CompanyPosition_2.ParentPK), " +
                                          "@Depth3 = MAX(CompanyPosition_3.ParentPK), @Depth4 = MAX(CompanyPosition_4.ParentPK), " +
                                          "@Depth5 = MAX(CompanyPosition_5.ParentPK), @Depth6 = MAX(CompanyPosition_6.ParentPK), " +
                                          "@Depth7 = MAX(CompanyPosition_7.ParentPK), @Depth8 = MAX(CompanyPosition_8.ParentPK), " +
                                          "@Depth9 = MAX(CompanyPosition_9.ParentPK), @Depth10 = MAX(CompanyPosition_10.ParentPK) " +
                                          "FROM CompanyPosition AS CompanyPosition_10 RIGHT JOIN (CompanyPosition AS CompanyPosition_9 " +
                                          "RIGHT JOIN (CompanyPosition AS CompanyPosition_8 RIGHT JOIN (CompanyPosition AS CompanyPosition_7 " +
                                          "RIGHT JOIN (CompanyPosition AS CompanyPosition_6 RIGHT JOIN (CompanyPosition AS CompanyPosition_5 " +
                                          "RIGHT JOIN (CompanyPosition AS CompanyPosition_4 RIGHT JOIN (CompanyPosition AS CompanyPosition_3 " +
                                          "RIGHT JOIN (CompanyPosition AS CompanyPosition_2 RIGHT JOIN (CompanyPosition AS CompanyPosition_1 " +
                                          "RIGHT JOIN CompanyPosition ON CompanyPosition_1.ParentPK = CompanyPosition.CompanyPositionPK) " +
                                          "ON CompanyPosition_2.ParentPK = CompanyPosition_1.CompanyPositionPK) ON CompanyPosition_3.ParentPK = CompanyPosition_2.CompanyPositionPK) " +
                                          "ON CompanyPosition_4.ParentPK = CompanyPosition_3.CompanyPositionPK) ON CompanyPosition_5.ParentPK = CompanyPosition_4.CompanyPositionPK) " +
                                          "ON CompanyPosition_6.ParentPK = CompanyPosition_5.CompanyPositionPK) ON CompanyPosition_7.ParentPK = CompanyPosition_6.CompanyPositionPK) " +
                                          "ON CompanyPosition_8.ParentPK = CompanyPosition_7.CompanyPositionPK) ON CompanyPosition_9.ParentPK = CompanyPosition_8.CompanyPositionPK) " +
                                          "ON CompanyPosition_10.ParentPK = CompanyPosition_9.CompanyPositionPK  " +
                                          "WHERE CompanyPosition.CompanyPositionPK = @CompanyPositionPK and CompanyPosition.Status = 2 " +
                                          "IF @Depth1 IS NULL " +
                                          "SET @Depth = 0  " +
                                          "ELSE IF @Depth2 IS NULL " +
                                          "SET @Depth = 1 " +
                                          "ELSE IF @Depth3 IS NULL " +
                                          "SET @Depth = 2 " +
                                          "ELSE IF @Depth4 IS NULL " +
                                          "SET @Depth = 3 " +
                                          "ELSE IF @Depth5 IS NULL " +
                                          "SET @Depth = 4 " +
                                          "ELSE IF @Depth6 IS NULL " +
                                          "SET @Depth = 5 " +
                                          "ELSE IF @Depth7 IS NULL " +
                                          "SET @Depth = 6 " +
                                          "ELSE IF @Depth8 IS NULL " +
                                          "SET @Depth = 7 " +
                                          "ELSE IF @Depth9 IS NULL " +
                                          "SET @Depth = 8  " +
                                          "ELSE IF @Depth10 IS NULL " +
                                          "SET @Depth = 9  " +
                                          "ELSE " +
                                          "SET @Depth = 0 " +
                                          "select @depth depth";
                        cmd.Parameters.AddWithValue("@CompanyPositionPK", _CompanyPositionPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["depth"]);
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

        public bool CheckHassAdd(int _pk, string _date, int _type)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select * from CompanyPosition where CompanyPositionPK = @PK and Status in (1,2) and Date = @Date and FeeType <> @Type";
                        cmd.Parameters.AddWithValue("@PK", _pk);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@Type", _type);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
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




        public List<CompanyPositionScheme> Get_DataDetailCompanyPositionScheme(int _categorySchemePK, int _companyPositionPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CompanyPositionScheme> L_model = new List<CompanyPositionScheme>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"

                        SELECT A.CategorySchemePK,B.ID CategorySchemeID,A.CompanyPositionPK,C.ID CompanyPositionID,A.FeePercent  FROM CompanyPositionScheme A
                        LEFT JOIN CategoryScheme B ON A.CategorySchemePK = B.CategorySchemePK AND B.status IN (1,2)
                        LEFT JOIN CompanyPosition C ON A.CompanyPositionPK = C.CompanyPositionPK AND C.status IN (1,2)
                        WHERE A.CategorySchemePK = @CategorySchemePK and A.CompanyPositionPK = @CompanyPositionPK
                        order by A.CategorySchemePK Desc ";

                        cmd.Parameters.AddWithValue("@CategorySchemePK", _categorySchemePK);
                        cmd.Parameters.AddWithValue("@CompanyPositionPK", _companyPositionPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    CompanyPositionScheme M_model = new CompanyPositionScheme();
                                    M_model.CategorySchemePK = Convert.ToInt32(dr["CategorySchemePK"]);
                                    M_model.CategorySchemeID = Convert.ToString(dr["CategorySchemeID"]);
                                    M_model.CompanyPositionPK = Convert.ToInt32(dr["CompanyPositionPK"]);
                                    M_model.CompanyPositionID = Convert.ToString(dr["CompanyPositionID"]);
                                    M_model.FeePercent = Convert.ToDecimal(dr["FeePercent"]);
                                    L_model.Add(M_model);


                                }
                            } return L_model;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public void Update_CompanyPositionScheme(CompanyPositionScheme _companyPositionScheme)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                        @"

                        Update CompanyPositionScheme set FeePercent = @FeePercent, LastUpdate = @TimeNow where CategorySchemePK = @CategorySchemePK and CompanyPositionPK = @CompanyPositionPK ";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@FeePercent", _companyPositionScheme.FeePercent);
                        cmd.Parameters.AddWithValue("@CategorySchemePK", _companyPositionScheme.CategorySchemePK);
                        cmd.Parameters.AddWithValue("@CompanyPositionPK", _companyPositionScheme.CompanyPositionPK);
                        cmd.Parameters.AddWithValue("@TimeNow", _datetimeNow);
                        cmd.ExecuteNonQuery();


                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public int Add_CompanyPositionScheme(CompanyPositionScheme _companyPositionScheme)
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

                        Declare @MaxPK int

                        select @MaxPK = Max(CompanyPositionSchemePK) from CompanyPositionScheme
                        set @maxPK = isnull(@maxPK,0)

                        Insert into CompanyPositionScheme (CompanyPositionSchemePK,HistoryPK,Status,CategorySchemePK,CompanyPositionPK,FeePercent,EntryUsersID,EntryTime,LastUpdate)

                        select @MaxPK + ROW_NUMBER() OVER(ORDER BY @CompanyPositionSchemePK ASC) CompanyPositionSchemePK,1,2,@CategorySchemePK,@CompanyPositionPK,@FeePercent,@EntryUsersID,@EntryTime,@LastUpdate

                        select @MaxPK + ROW_NUMBER() OVER(ORDER BY @CompanyPositionSchemePK ASC) Result

                        ";


                        cmd.Parameters.AddWithValue("@CompanyPositionSchemePK", _companyPositionScheme.CompanyPositionSchemePK);
                        cmd.Parameters.AddWithValue("@CategorySchemePK", _companyPositionScheme.CategorySchemePK);
                        cmd.Parameters.AddWithValue("@CompanyPositionPK", _companyPositionScheme.CompanyPositionPK);
                        cmd.Parameters.AddWithValue("@FeePercent", _companyPositionScheme.FeePercent);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _companyPositionScheme.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["Result"]);

                            }
                            return 0;
                        }
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public bool Validate_MaxPercentCompanyPosition(CompanyPositionScheme _companyPositionScheme)
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

                        DECLARE @CurrentFeePercent NUMERIC(8,4)
                        Declare @OldPercent numeric(18,4)
                        IF EXISTS(
                        select isnull(FeePercent,0) from CompanyPositionScheme where CompanyPositionSchemePK = @CompanyPositionSchemePK and status = 2
                        )
                        BEGIN
	                        select @OldPercent = isnull(FeePercent,0) from CompanyPositionScheme where CompanyPositionSchemePK = @CompanyPositionSchemePK and status = 2
                        END
                        ELSE
                        BEGIN
	                        select @OldPercent = 0
                        END



                        SELECT @CurrentFeePercent =  SUM(ISNULL(FeePercent,0)) - @OldPercent FROM dbo.CompanyPositionScheme WHERE CategorySchemePK = @CategorySchemePK and status = 2

                        IF (ISNULL(@CurrentFeePercent,0) + @NewFeePercent) > 100
                        BEGIN
	                        SELECT 1 Result
                        END
                        ELSE
                        BEGIN
	                        SELECT 0 Result
                        END ";

                        cmd.Parameters.AddWithValue("@CompanyPositionSchemePK", _companyPositionScheme.CompanyPositionSchemePK);
                        cmd.Parameters.AddWithValue("@CategorySchemePK", _companyPositionScheme.CategorySchemePK);
                        cmd.Parameters.AddWithValue("@NewFeePercent", _companyPositionScheme.FeePercent);
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


        public bool Validate_CheckHasAddCompanyPositionScheme(int _categorySchemePK, int _companyPositionPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select * from CompanyPositionScheme where CategorySchemePK = @CategorySchemePK and CompanyPositionPK = @CompanyPositionPK and Status = 2 ";
                        cmd.Parameters.AddWithValue("@CategorySchemePK", _categorySchemePK);
                        cmd.Parameters.AddWithValue("@CompanyPositionPK", _companyPositionPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
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


        public List<CompanyPositionCombo> CompanyPositionScheme_Combo()
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CompanyPositionCombo> L_CompanyPosition = new List<CompanyPositionCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @"SELECT  CompanyPositionSchemePK,B.ID + ' - ' + C.ID ID, B.Name + ' - ' + C.Name Name FROM [CompanyPositionScheme] A
                                            left join CategoryScheme B on A.CategorySchemePK = B.CategorySchemePK and B.status = 2
                                            left join CompanyPosition C on A.CompanyPositionPK = C.CompanyPositionPK and C.status = 2                                            
                                            where A.status = 2 ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CompanyPositionCombo M_CompanyPosition = new CompanyPositionCombo();
                                    M_CompanyPosition.CompanyPositionSchemePK = Convert.ToInt32(dr["CompanyPositionSchemePK"]);
                                    M_CompanyPosition.ID = Convert.ToString(dr["ID"]);
                                    M_CompanyPosition.Name = Convert.ToString(dr["Name"]);
                                    L_CompanyPosition.Add(M_CompanyPosition);
                                }

                            }
                        }
                        return L_CompanyPosition;
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