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
    public class MISCostCenterReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[MISCostCenter] " +
                            "([MISCostCenterPK],[HistoryPK],[Status],[ID],[Name],[Description],[Groups],[Levels],[ParentPK],[Depth]," +
                            "[ParentPK1],[ParentPK2],[ParentPK3],[ParentPK4],[ParentPK5],[ParentPK6],[ParentPK7],[ParentPK8],[ParentPK9],[Priority],";
        string _paramaterCommand = "@ID,@Name,@Description,@Groups,@Levels,@ParentPK,@Depth,@ParentPK1,@ParentPK2,@ParentPK3,@ParentPK4,@ParentPK5,@ParentPK6,@ParentPK7,@ParentPK8,@ParentPK9,@Priority,";

        //2
        private MISCostCenter setMISCostCenter(SqlDataReader dr)
        {
            MISCostCenter M_MISCostCenter = new MISCostCenter();
            M_MISCostCenter.MISCostCenterPK = Convert.ToInt32(dr["MISCostCenterPK"]);
            M_MISCostCenter.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_MISCostCenter.Status = Convert.ToInt32(dr["Status"]);
            M_MISCostCenter.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_MISCostCenter.Notes = Convert.ToString(dr["Notes"]);
            M_MISCostCenter.ID = dr["ID"].ToString();
            M_MISCostCenter.Name = dr["Name"].ToString(); 
            M_MISCostCenter.Description = dr["Description"].ToString();
            M_MISCostCenter.Groups = Convert.ToBoolean(dr["Groups"]);
            M_MISCostCenter.Levels = Convert.ToInt32(dr["Levels"]);
            M_MISCostCenter.ParentPK = Convert.ToInt32(dr["ParentPK"]);
            M_MISCostCenter.ParentID = dr["ParentID"].ToString();
            M_MISCostCenter.ParentName = dr["ParentName"].ToString();
            M_MISCostCenter.Depth = Convert.ToInt32(dr["Depth"]);
            M_MISCostCenter.ParentPK1 = Convert.ToInt32(dr["ParentPK1"]);
            M_MISCostCenter.ParentPK2 = Convert.ToInt32(dr["ParentPK2"]);
            M_MISCostCenter.ParentPK3 = Convert.ToInt32(dr["ParentPK3"]);
            M_MISCostCenter.ParentPK4 = Convert.ToInt32(dr["ParentPK4"]);
            M_MISCostCenter.ParentPK5 = Convert.ToInt32(dr["ParentPK5"]);
            M_MISCostCenter.ParentPK6 = Convert.ToInt32(dr["ParentPK6"]);
            M_MISCostCenter.ParentPK7 = Convert.ToInt32(dr["ParentPK7"]);
            M_MISCostCenter.ParentPK8 = Convert.ToInt32(dr["ParentPK8"]);
            M_MISCostCenter.ParentPK9 = Convert.ToInt32(dr["ParentPK9"]);
            M_MISCostCenter.Priority = Convert.ToInt32(dr["Priority"]);
            M_MISCostCenter.EntryUsersID = dr["EntryUsersID"].ToString();
            M_MISCostCenter.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_MISCostCenter.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_MISCostCenter.VoidUsersID = dr["VoidUsersID"].ToString();
            M_MISCostCenter.EntryTime = dr["EntryTime"].ToString();
            M_MISCostCenter.UpdateTime = dr["UpdateTime"].ToString();
            M_MISCostCenter.ApprovedTime = dr["ApprovedTime"].ToString();
            M_MISCostCenter.VoidTime = dr["VoidTime"].ToString();
            M_MISCostCenter.DBUserID = dr["DBUserID"].ToString();
            M_MISCostCenter.DBTerminalID = dr["DBTerminalID"].ToString();
            M_MISCostCenter.LastUpdate = dr["LastUpdate"].ToString();
            M_MISCostCenter.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_MISCostCenter;
        }

        public List<MISCostCenter> MISCostCenter_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MISCostCenter> L_MISCostCenter = new List<MISCostCenter>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID ParentID,B.Name ParentName, A.* from MISCostCenter A 
                                                left join MISCostCenter B on A.ParentPK = B.MISCostCenterPK and B.status in (1,2)
                                                where A.status = @status order by A.MISCostCenterPK";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID ParentID,B.Name ParentName, A.* from MISCostCenter A 
                                                left join MISCostCenter B on A.ParentPK = B.MISCostCenterPK and B.status in (1,2)
                                                order by A.MISCostCenterPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MISCostCenter.Add(setMISCostCenter(dr));
                                }
                            }
                            return L_MISCostCenter;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int MISCostCenter_Add(MISCostCenter _MISCostCenter, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],lastupdate)" +
                                 "Select isnull(max(MISCostCenterPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate From MISCostCenter";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _MISCostCenter.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(MISCostCenterPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate From MISCostCenter";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _MISCostCenter.ID);
                        cmd.Parameters.AddWithValue("@Name", _MISCostCenter.Name);
                        cmd.Parameters.AddWithValue("@Description", _MISCostCenter.Description);
                        cmd.Parameters.AddWithValue("@Groups", _MISCostCenter.Groups);
                        cmd.Parameters.AddWithValue("@Levels", _MISCostCenter.Levels);
                        cmd.Parameters.AddWithValue("@ParentPK", _MISCostCenter.ParentPK); 
                        cmd.Parameters.AddWithValue("@Depth", _MISCostCenter.Depth);
                        cmd.Parameters.AddWithValue("@ParentPK1", _MISCostCenter.ParentPK1);
                        cmd.Parameters.AddWithValue("@ParentPK2", _MISCostCenter.ParentPK2);
                        cmd.Parameters.AddWithValue("@ParentPK3", _MISCostCenter.ParentPK3);
                        cmd.Parameters.AddWithValue("@ParentPK4", _MISCostCenter.ParentPK4);
                        cmd.Parameters.AddWithValue("@ParentPK5", _MISCostCenter.ParentPK5);
                        cmd.Parameters.AddWithValue("@ParentPK6", _MISCostCenter.ParentPK6);
                        cmd.Parameters.AddWithValue("@ParentPK7", _MISCostCenter.ParentPK7);
                        cmd.Parameters.AddWithValue("@ParentPK8", _MISCostCenter.ParentPK8);
                        cmd.Parameters.AddWithValue("@ParentPK9", _MISCostCenter.ParentPK9);
                        cmd.Parameters.AddWithValue("@Priority", _MISCostCenter.Priority);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _MISCostCenter.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "MISCostCenter");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int MISCostCenter_Update(MISCostCenter _MISCostCenter, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_MISCostCenter.MISCostCenterPK, _MISCostCenter.HistoryPK, "MISCostCenter");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update MISCostCenter set status=2, Notes=@Notes,ID=@ID,Name=@Name,Description=@Description,Groups=@Groups," +
                                "Levels=@Levels,ParentPK=@ParentPK,Depth=@Depth,ParentPK1=@ParentPK1,ParentPK2=@ParentPK2,ParentPK3=@ParentPK3,ParentPK4=@ParentPK4," +
                                "ParentPK5=@ParentPK5,ParentPK6=@ParentPK6,ParentPK7=@ParentPK7,ParentPK8=@ParentPK8,ParentPK9=@ParentPK9,Priority=@Priority," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where MISCostCenterPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _MISCostCenter.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _MISCostCenter.MISCostCenterPK);
                            cmd.Parameters.AddWithValue("@Notes", _MISCostCenter.Notes);
                            cmd.Parameters.AddWithValue("@ID", _MISCostCenter.ID);
                            cmd.Parameters.AddWithValue("@Name", _MISCostCenter.Name);
                            cmd.Parameters.AddWithValue("@Description", _MISCostCenter.Description);
                            cmd.Parameters.AddWithValue("@Groups", _MISCostCenter.Groups);
                            cmd.Parameters.AddWithValue("@Levels", _MISCostCenter.Levels);
                            cmd.Parameters.AddWithValue("@ParentPK", _MISCostCenter.ParentPK);
                            cmd.Parameters.AddWithValue("@Depth", _MISCostCenter.Depth);
                            cmd.Parameters.AddWithValue("@ParentPK1", _MISCostCenter.ParentPK1);
                            cmd.Parameters.AddWithValue("@ParentPK2", _MISCostCenter.ParentPK2);
                            cmd.Parameters.AddWithValue("@ParentPK3", _MISCostCenter.ParentPK3);
                            cmd.Parameters.AddWithValue("@ParentPK4", _MISCostCenter.ParentPK4);
                            cmd.Parameters.AddWithValue("@ParentPK5", _MISCostCenter.ParentPK5);
                            cmd.Parameters.AddWithValue("@ParentPK6", _MISCostCenter.ParentPK6);
                            cmd.Parameters.AddWithValue("@ParentPK7", _MISCostCenter.ParentPK7);
                            cmd.Parameters.AddWithValue("@ParentPK8", _MISCostCenter.ParentPK8);
                            cmd.Parameters.AddWithValue("@ParentPK9", _MISCostCenter.ParentPK9);
                            cmd.Parameters.AddWithValue("@Priority", _MISCostCenter.Priority);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _MISCostCenter.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _MISCostCenter.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update MISCostCenter set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate = @lastupdate where MISCostCenterPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _MISCostCenter.MISCostCenterPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _MISCostCenter.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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
                                cmd.CommandText = "Update MISCostCenter set Notes=@Notes,ID=@ID,Name=@Name,Description=@Description,Groups=@Groups," +
                                    "Levels=@Levels,ParentPK=@ParentPK,Depth=@Depth,ParentPK1=@ParentPK1,ParentPK2=@ParentPK2,ParentPK3=@ParentPK3,ParentPK4=@ParentPK4," +
                                    "ParentPK5=@ParentPK5,ParentPK6=@ParentPK6,ParentPK7=@ParentPK7,ParentPK8=@ParentPK8,ParentPK9=@ParentPK9,Priority=@Priority," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where MISCostCenterPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _MISCostCenter.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _MISCostCenter.MISCostCenterPK);
                                cmd.Parameters.AddWithValue("@Notes", _MISCostCenter.Notes);
                                cmd.Parameters.AddWithValue("@ID", _MISCostCenter.ID);
                                cmd.Parameters.AddWithValue("@Name", _MISCostCenter.Name);
                                cmd.Parameters.AddWithValue("@Description", _MISCostCenter.Description);
                                cmd.Parameters.AddWithValue("@Groups", _MISCostCenter.Groups);
                                cmd.Parameters.AddWithValue("@Levels", _MISCostCenter.Levels);
                                cmd.Parameters.AddWithValue("@ParentPK", _MISCostCenter.ParentPK);
                                cmd.Parameters.AddWithValue("@Depth", _MISCostCenter.Depth);
                                cmd.Parameters.AddWithValue("@ParentPK1", _MISCostCenter.ParentPK1);
                                cmd.Parameters.AddWithValue("@ParentPK2", _MISCostCenter.ParentPK2);
                                cmd.Parameters.AddWithValue("@ParentPK3", _MISCostCenter.ParentPK3);
                                cmd.Parameters.AddWithValue("@ParentPK4", _MISCostCenter.ParentPK4);
                                cmd.Parameters.AddWithValue("@ParentPK5", _MISCostCenter.ParentPK5);
                                cmd.Parameters.AddWithValue("@ParentPK6", _MISCostCenter.ParentPK6);
                                cmd.Parameters.AddWithValue("@ParentPK7", _MISCostCenter.ParentPK7);
                                cmd.Parameters.AddWithValue("@ParentPK8", _MISCostCenter.ParentPK8);
                                cmd.Parameters.AddWithValue("@ParentPK9", _MISCostCenter.ParentPK9);
                                cmd.Parameters.AddWithValue("@Priority", _MISCostCenter.Priority);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _MISCostCenter.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_MISCostCenter.MISCostCenterPK, "MISCostCenter");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From MISCostCenter where MISCostCenterPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _MISCostCenter.MISCostCenterPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _MISCostCenter.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _MISCostCenter.ID);
                                cmd.Parameters.AddWithValue("@Name", _MISCostCenter.Name);
                                cmd.Parameters.AddWithValue("@Description", _MISCostCenter.Description);
                                cmd.Parameters.AddWithValue("@Groups", _MISCostCenter.Groups);
                                cmd.Parameters.AddWithValue("@Levels", _MISCostCenter.Levels);
                                cmd.Parameters.AddWithValue("@ParentPK", _MISCostCenter.ParentPK);
                                cmd.Parameters.AddWithValue("@Depth", _MISCostCenter.Depth);
                                cmd.Parameters.AddWithValue("@ParentPK1", _MISCostCenter.ParentPK1);
                                cmd.Parameters.AddWithValue("@ParentPK2", _MISCostCenter.ParentPK2);
                                cmd.Parameters.AddWithValue("@ParentPK3", _MISCostCenter.ParentPK3);
                                cmd.Parameters.AddWithValue("@ParentPK4", _MISCostCenter.ParentPK4);
                                cmd.Parameters.AddWithValue("@ParentPK5", _MISCostCenter.ParentPK5);
                                cmd.Parameters.AddWithValue("@ParentPK6", _MISCostCenter.ParentPK6);
                                cmd.Parameters.AddWithValue("@ParentPK7", _MISCostCenter.ParentPK7);
                                cmd.Parameters.AddWithValue("@ParentPK8", _MISCostCenter.ParentPK8);
                                cmd.Parameters.AddWithValue("@ParentPK9", _MISCostCenter.ParentPK9);
                                cmd.Parameters.AddWithValue("@Priority", _MISCostCenter.Priority);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _MISCostCenter.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update MISCostCenter set status= 4,Notes=@Notes," +
                                    "lastupdate=@lastupdate where MISCostCenterPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _MISCostCenter.Notes);
                                cmd.Parameters.AddWithValue("@PK", _MISCostCenter.MISCostCenterPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _MISCostCenter.HistoryPK);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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

        public void MISCostCenter_Approved(MISCostCenter _MISCostCenter)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MISCostCenter set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime, lastupdate=@lastupdate " +
                            "where MISCostCenterPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _MISCostCenter.MISCostCenterPK);
                        cmd.Parameters.AddWithValue("@historyPK", _MISCostCenter.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _MISCostCenter.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update MISCostCenter set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate = @lastupdate where MISCostCenterPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _MISCostCenter.MISCostCenterPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _MISCostCenter.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void MISCostCenter_Reject(MISCostCenter _MISCostCenter)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MISCostCenter set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where MISCostCenterPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _MISCostCenter.MISCostCenterPK);
                        cmd.Parameters.AddWithValue("@historyPK", _MISCostCenter.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _MISCostCenter.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update MISCostCenter set status= 2,lastupdate=@lastupdate where MISCostCenterPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _MISCostCenter.MISCostCenterPK);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void MISCostCenter_Void(MISCostCenter _MISCostCenter)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MISCostCenter set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where MISCostCenterPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _MISCostCenter.MISCostCenterPK);
                        cmd.Parameters.AddWithValue("@historyPK", _MISCostCenter.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _MISCostCenter.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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

        public List<MISCostCenterCombo> MISCostCenter_ComboRpt()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MISCostCenterCombo> L_MISCostCenter = new List<MISCostCenterCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  MISCostCenterPK,ID + ' - ' + Name as ID, Name FROM [MISCostCenter]  where status = 2 union all select 0,'All', '' order by MISCostCenterPK,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    MISCostCenterCombo M_MISCostCenter = new MISCostCenterCombo();
                                    M_MISCostCenter.MISCostCenterPK = Convert.ToInt32(dr["MISCostCenterPK"]);
                                    M_MISCostCenter.ID = Convert.ToString(dr["ID"]);
                                    M_MISCostCenter.Name = Convert.ToString(dr["Name"]);
                                    L_MISCostCenter.Add(M_MISCostCenter);
                                }
                            }
                            return L_MISCostCenter;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<MISCostCenterCombo> MISCostCenter_Combo()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MISCostCenterCombo> L_MISCostCenter = new List<MISCostCenterCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  MISCostCenterPK,ID + ' - ' + Name as [ID], Name FROM [MISCostCenter]   where status = 2 and groups = 0  order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    MISCostCenterCombo M_MISCostCenter = new MISCostCenterCombo();
                                    M_MISCostCenter.MISCostCenterPK = Convert.ToInt32(dr["MISCostCenterPK"]);
                                    M_MISCostCenter.ID = Convert.ToString(dr["ID"]);
                                    M_MISCostCenter.Name = Convert.ToString(dr["Name"]);
                                    L_MISCostCenter.Add(M_MISCostCenter);
                                }
                            }
                            return L_MISCostCenter;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<MISCostCenterCombo> MISCostCenter_ComboGroupOnly()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MISCostCenterCombo> L_MISCostCenter = new List<MISCostCenterCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  MISCostCenterPK,ID + ' - ' + Name as [ID], Name FROM [MISCostCenter]  where Groups = 1 and status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    MISCostCenterCombo M_MISCostCenter = new MISCostCenterCombo();
                                    M_MISCostCenter.MISCostCenterPK = Convert.ToInt32(dr["MISCostCenterPK"]);
                                    M_MISCostCenter.ID = Convert.ToString(dr["ID"]);
                                    M_MISCostCenter.Name = Convert.ToString(dr["Name"]);
                                    L_MISCostCenter.Add(M_MISCostCenter);
                                }
                            }
                            return L_MISCostCenter;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public bool MISCostCenter_UpdateParentAndDepth()
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE MISCostCenter SET " +
                                                "MISCostCenter.ParentPK1 = isnull(MISCostCenter_1.MISCostCenterPK,0), MISCostCenter.ParentPK2 = isnull(MISCostCenter_2.MISCostCenterPK,0), " +
                                                "MISCostCenter.ParentPK3 = isnull(MISCostCenter_3.MISCostCenterPK,0), MISCostCenter.ParentPK4 = isnull(MISCostCenter_4.MISCostCenterPK,0), " +
                                                "MISCostCenter.ParentPK5 = isnull(MISCostCenter_5.MISCostCenterPK,0), MISCostCenter.ParentPK6 = isnull(MISCostCenter_6.MISCostCenterPK,0), " +
                                                "MISCostCenter.ParentPK7 = isnull(MISCostCenter_7.MISCostCenterPK,0), MISCostCenter.ParentPK8 = isnull(MISCostCenter_8.MISCostCenterPK,0), " +
                                                "MISCostCenter.ParentPK9 = isnull(MISCostCenter_9.MISCostCenterPK,0)  " +
                                                "FROM MISCostCenter " +
                                                "LEFT JOIN MISCostCenter AS MISCostCenter_1 ON MISCostCenter.ParentPK = MISCostCenter_1.MISCostCenterPK " +
                                                "LEFT JOIN MISCostCenter AS MISCostCenter_2 ON MISCostCenter_1.ParentPK = MISCostCenter_2.MISCostCenterPK " +
                                                "LEFT JOIN MISCostCenter AS MISCostCenter_3 ON MISCostCenter_2.ParentPK = MISCostCenter_3.MISCostCenterPK " +
                                                "LEFT JOIN MISCostCenter AS MISCostCenter_4 ON MISCostCenter_3.ParentPK = MISCostCenter_4.MISCostCenterPK " +
                                                "LEFT JOIN MISCostCenter AS MISCostCenter_5 ON MISCostCenter_4.ParentPK = MISCostCenter_5.MISCostCenterPK " +
                                                "LEFT JOIN MISCostCenter AS MISCostCenter_6 ON MISCostCenter_5.ParentPK = MISCostCenter_6.MISCostCenterPK " +
                                                "LEFT JOIN MISCostCenter AS MISCostCenter_7 ON MISCostCenter_6.ParentPK = MISCostCenter_7.MISCostCenterPK " +
                                                "LEFT JOIN MISCostCenter AS MISCostCenter_8 ON MISCostCenter_7.ParentPK = MISCostCenter_8.MISCostCenterPK " +
                                                "LEFT JOIN MISCostCenter AS MISCostCenter_9 ON MISCostCenter_8.ParentPK = MISCostCenter_9.MISCostCenterPK Where MISCostCenter.Status = 2 ";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Select MISCostCenterPK From MISCostCenter Where Status = 2 and Groups = 1";
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
                                                cmdSubQuery.CommandText = "Update MISCostCenter set Depth = @Depth, lastupdate=@lastupdate Where MISCostCenterPK = @MISCostCenterPK and Status = 2";
                                                cmdSubQuery.Parameters.AddWithValue("@Depth", GetMISCostCenterDepth(Convert.ToInt32(dr["MISCostCenterPK"])));
                                                cmdSubQuery.Parameters.AddWithValue("@MISCostCenterPK", Convert.ToInt32(dr["MISCostCenterPK"]));
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

        private int GetMISCostCenterDepth(int _MISCostCenterPK)
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
                                          "SELECT @Depth1 = MAX(MISCostCenter_1.ParentPK), @Depth2 = MAX(MISCostCenter_2.ParentPK), " +
                                          "@Depth3 = MAX(MISCostCenter_3.ParentPK), @Depth4 = MAX(MISCostCenter_4.ParentPK), " +
                                          "@Depth5 = MAX(MISCostCenter_5.ParentPK), @Depth6 = MAX(MISCostCenter_6.ParentPK), " +
                                          "@Depth7 = MAX(MISCostCenter_7.ParentPK), @Depth8 = MAX(MISCostCenter_8.ParentPK), " +
                                          "@Depth9 = MAX(MISCostCenter_9.ParentPK), @Depth10 = MAX(MISCostCenter_10.ParentPK) " +
                                          "FROM MISCostCenter AS MISCostCenter_10 RIGHT JOIN (MISCostCenter AS MISCostCenter_9 " +
                                          "RIGHT JOIN (MISCostCenter AS MISCostCenter_8 RIGHT JOIN (MISCostCenter AS MISCostCenter_7 " +
                                          "RIGHT JOIN (MISCostCenter AS MISCostCenter_6 RIGHT JOIN (MISCostCenter AS MISCostCenter_5 " +
                                          "RIGHT JOIN (MISCostCenter AS MISCostCenter_4 RIGHT JOIN (MISCostCenter AS MISCostCenter_3 " +
                                          "RIGHT JOIN (MISCostCenter AS MISCostCenter_2 RIGHT JOIN (MISCostCenter AS MISCostCenter_1 " +
                                          "RIGHT JOIN MISCostCenter ON MISCostCenter_1.ParentPK = MISCostCenter.MISCostCenterPK) " +
                                          "ON MISCostCenter_2.ParentPK = MISCostCenter_1.MISCostCenterPK) ON MISCostCenter_3.ParentPK = MISCostCenter_2.MISCostCenterPK) " +
                                          "ON MISCostCenter_4.ParentPK = MISCostCenter_3.MISCostCenterPK) ON MISCostCenter_5.ParentPK = MISCostCenter_4.MISCostCenterPK) " +
                                          "ON MISCostCenter_6.ParentPK = MISCostCenter_5.MISCostCenterPK) ON MISCostCenter_7.ParentPK = MISCostCenter_6.MISCostCenterPK) " +
                                          "ON MISCostCenter_8.ParentPK = MISCostCenter_7.MISCostCenterPK) ON MISCostCenter_9.ParentPK = MISCostCenter_8.MISCostCenterPK) " +
                                          "ON MISCostCenter_10.ParentPK = MISCostCenter_9.MISCostCenterPK  " +
                                          "WHERE MISCostCenter.MISCostCenterPK = @MISCostCenterPK and MISCostCenter.Status = 2 " +
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
                        cmd.Parameters.AddWithValue("@MISCostCenterPK", _MISCostCenterPK);
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

    }
}