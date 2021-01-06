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
    public class CustodianReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[Custodian] " +
                            "([CustodianPK],[HistoryPK],[Status],[ID],[Name],[Type],[Groups],[Levels],[ParentPK]," +
                            "[Show],[ParentPK1],[ParentPK2],[ParentPK3],[ParentPK4],[ParentPK5],[MKBDMapping],";
        string _paramaterCommand = "@ID,@Name,@Type,@Groups,@Levels,@ParentPK," +
                            "@Show,@ParentPK1,@ParentPK2,@ParentPK3,@ParentPK4,@ParentPK5,@MKBDMapping,";

        //2
        private Custodian setCustodian(SqlDataReader dr)
        {
            Custodian M_Custodian = new Custodian();
            M_Custodian.CustodianPK = Convert.ToInt32(dr["CustodianPK"]);
            M_Custodian.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Custodian.Status = Convert.ToInt32(dr["Status"]);
            M_Custodian.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Custodian.Notes = Convert.ToString(dr["Notes"]);
            M_Custodian.ID = dr["ID"].ToString();
            M_Custodian.Name = dr["Name"].ToString();
            M_Custodian.Type = Convert.ToString(dr["Type"]);
            M_Custodian.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_Custodian.Groups = Convert.ToBoolean(dr["Groups"]);
            M_Custodian.Levels = Convert.ToInt32(dr["Levels"]);
            M_Custodian.ParentPK = Convert.ToInt32(dr["ParentPK"]);
            M_Custodian.ParentID = Convert.ToString(dr["ParentID"]);
            M_Custodian.ParentName = Convert.ToString(dr["ParentName"]);
            M_Custodian.Depth = Convert.ToInt32(dr["Depth"]);
            M_Custodian.Show = Convert.ToBoolean(dr["Show"]);
            M_Custodian.ParentPK1 = Convert.ToInt32(dr["ParentPK1"]);
            M_Custodian.ParentPK2 = Convert.ToInt32(dr["ParentPK2"]);
            M_Custodian.ParentPK3 = Convert.ToInt32(dr["ParentPK3"]);
            M_Custodian.ParentPK4 = Convert.ToInt32(dr["ParentPK4"]);
            M_Custodian.ParentPK5 = Convert.ToInt32(dr["ParentPK5"]);
            M_Custodian.MKBDMapping = Convert.ToInt32(dr["MKBDMapping"]);
            M_Custodian.EntryUsersID = Convert.ToString(dr["EntryUsersID"]);
            M_Custodian.UpdateUsersID = Convert.ToString(dr["UpdateUsersID"]);
            M_Custodian.ApprovedUsersID = Convert.ToString(dr["ApprovedUsersID"]);
            M_Custodian.VoidUsersID = Convert.ToString(dr["VoidUsersID"]);
            M_Custodian.EntryTime = Convert.ToString(dr["EntryTime"]);
            M_Custodian.UpdateTime = Convert.ToString(dr["UpdateTime"]);
            M_Custodian.ApprovedTime = Convert.ToString(dr["ApprovedTime"]);
            M_Custodian.VoidTime = Convert.ToString(dr["VoidTime"]);
            M_Custodian.DBUserID = Convert.ToString(dr["DBUserID"]);
            M_Custodian.DBTerminalID = Convert.ToString(dr["DBTerminalID"]);
            M_Custodian.LastUpdate = Convert.ToString(dr["LastUpdate"]);
            M_Custodian.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_Custodian;
        }

        public List<Custodian> Custodian_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Custodian> L_Custodian = new List<Custodian>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = " Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne TypeDesc,ZA.ID ParentID,ZA.Name ParentName, A.* from Custodian A left join   " +
                            " Custodian ZA on A.ParentPK = ZA.CustodianPK  and ZA.status in (1,2) left join " +
                            " MasterValue MV on A.Type=MV.Code and MV.ID ='CustodianType' " +
                            " where A.status = @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = " Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne TypeDesc,ZA.ID ParentID,ZA.Name ParentName, A.* from Custodian A left join   " +
                            " Custodian ZA on A.ParentPK = ZA.CustodianPK  and ZA.status in (1,2) left join " +
                            " MasterValue MV on A.Type=MV.Code and MV.ID ='CustodianType' ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Custodian.Add(setCustodian(dr));
                                }
                            }
                            return L_Custodian;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Custodian_Add(Custodian _custodian, bool _havePrivillege)
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
                                 "Select isnull(max(CustodianPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from Custodian";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _custodian.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(CustodianPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from Custodian";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _custodian.ID);
                        cmd.Parameters.AddWithValue("@Name", _custodian.Name);
                        cmd.Parameters.AddWithValue("@Type", _custodian.Type);
                        cmd.Parameters.AddWithValue("@Groups", _custodian.Groups);
                        cmd.Parameters.AddWithValue("@Levels", _custodian.Levels);
                        cmd.Parameters.AddWithValue("@ParentPK", _custodian.ParentPK);
                        cmd.Parameters.AddWithValue("@Show", _custodian.Show);
                        cmd.Parameters.AddWithValue("@ParentPK1", _custodian.ParentPK1);
                        cmd.Parameters.AddWithValue("@ParentPK2", _custodian.ParentPK2);
                        cmd.Parameters.AddWithValue("@ParentPK3", _custodian.ParentPK3);
                        cmd.Parameters.AddWithValue("@ParentPK4", _custodian.ParentPK4);
                        cmd.Parameters.AddWithValue("@ParentPK5", _custodian.ParentPK5);
                        cmd.Parameters.AddWithValue("@MKBDMapping", _custodian.MKBDMapping);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _custodian.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "Custodian");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int Custodian_Update(Custodian _custodian, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_custodian.CustodianPK, _custodian.HistoryPK, "Custodian");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Custodian set status=2,Notes=@Notes,ID=@ID,Name=@Name,Type=@Type,Groups=@Groups," +
                                "Levels=@Levels,ParentPK=@ParentPK," +
                                "Show=@Show,ParentPK1=@ParentPK1,ParentPK2=@ParentPK2,ParentPK3=@ParentPK3,ParentPK4=@ParentPK4,ParentPK5=@ParentPK5,MKBDMapping=@MKBDMapping," +
                                "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where CustodianPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _custodian.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _custodian.CustodianPK);
                            cmd.Parameters.AddWithValue("@ID", _custodian.ID);
                            cmd.Parameters.AddWithValue("@Notes", _custodian.Notes);
                            cmd.Parameters.AddWithValue("@Name", _custodian.Name);
                            cmd.Parameters.AddWithValue("@Type", _custodian.Type);
                            cmd.Parameters.AddWithValue("@Groups", _custodian.Groups);
                            cmd.Parameters.AddWithValue("@Levels", _custodian.Levels);
                            cmd.Parameters.AddWithValue("@ParentPK", _custodian.ParentPK);
                            cmd.Parameters.AddWithValue("@Show", _custodian.Show);
                            cmd.Parameters.AddWithValue("@ParentPK1", _custodian.ParentPK1);
                            cmd.Parameters.AddWithValue("@ParentPK2", _custodian.ParentPK2);
                            cmd.Parameters.AddWithValue("@ParentPK3", _custodian.ParentPK3);
                            cmd.Parameters.AddWithValue("@ParentPK4", _custodian.ParentPK4);
                            cmd.Parameters.AddWithValue("@ParentPK5", _custodian.ParentPK5);
                            cmd.Parameters.AddWithValue("@MKBDMapping", _custodian.MKBDMapping);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _custodian.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _custodian.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Custodian set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where CustodianPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _custodian.CustodianPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _custodian.EntryUsersID);
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
                                cmd.CommandText = "Update Custodian set Notes=@Notes,ID=@ID,Name=@Name,Type=@Type,Groups=@Groups," +
                                    "Levels=@Levels,ParentPK=@ParentPK, " +
                                    "Show=@Show,ParentPK1=@ParentPK1,ParentPK2=@ParentPK2,ParentPK3=@ParentPK3,ParentPK4=@ParentPK4,ParentPK5=@ParentPK5,MKBDMapping=@MKBDMapping," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where CustodianPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _custodian.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _custodian.CustodianPK);
                                cmd.Parameters.AddWithValue("@ID", _custodian.ID);
                                cmd.Parameters.AddWithValue("@Notes", _custodian.Notes);
                                cmd.Parameters.AddWithValue("@Name", _custodian.Name);
                                cmd.Parameters.AddWithValue("@Type", _custodian.Type);
                                cmd.Parameters.AddWithValue("@Groups", _custodian.Groups);
                                cmd.Parameters.AddWithValue("@Levels", _custodian.Levels);
                                cmd.Parameters.AddWithValue("@ParentPK", _custodian.ParentPK);
                                cmd.Parameters.AddWithValue("@Show", _custodian.Show);
                                cmd.Parameters.AddWithValue("@ParentPK1", _custodian.ParentPK1);
                                cmd.Parameters.AddWithValue("@ParentPK2", _custodian.ParentPK2);
                                cmd.Parameters.AddWithValue("@ParentPK3", _custodian.ParentPK3);
                                cmd.Parameters.AddWithValue("@ParentPK4", _custodian.ParentPK4);
                                cmd.Parameters.AddWithValue("@ParentPK5", _custodian.ParentPK5);
                                cmd.Parameters.AddWithValue("@MKBDMapping", _custodian.MKBDMapping);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _custodian.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_custodian.CustodianPK, "Custodian");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Custodian where CustodianPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _custodian.CustodianPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _custodian.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _custodian.ID);
                                cmd.Parameters.AddWithValue("@Name", _custodian.Name);
                                cmd.Parameters.AddWithValue("@Type", _custodian.Type);
                                cmd.Parameters.AddWithValue("@Groups", _custodian.Groups);
                                cmd.Parameters.AddWithValue("@Levels", _custodian.Levels);
                                cmd.Parameters.AddWithValue("@ParentPK", _custodian.ParentPK);
                                cmd.Parameters.AddWithValue("@Show", _custodian.Show);
                                cmd.Parameters.AddWithValue("@ParentPK1", _custodian.ParentPK1);
                                cmd.Parameters.AddWithValue("@ParentPK2", _custodian.ParentPK2);
                                cmd.Parameters.AddWithValue("@ParentPK3", _custodian.ParentPK3);
                                cmd.Parameters.AddWithValue("@ParentPK4", _custodian.ParentPK4);
                                cmd.Parameters.AddWithValue("@ParentPK5", _custodian.ParentPK5);
                                cmd.Parameters.AddWithValue("@MKBDMapping", _custodian.MKBDMapping);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _custodian.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Custodian set status = 4, Notes=@Notes, " +
                                " lastupdate=@lastupdate where CustodianPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _custodian.Notes);
                                cmd.Parameters.AddWithValue("@PK", _custodian.CustodianPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _custodian.HistoryPK);
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

        public void Custodian_Approved(Custodian _custodian)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Custodian set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where CustodianPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _custodian.CustodianPK);
                        cmd.Parameters.AddWithValue("@historyPK", _custodian.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _custodian.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Custodian set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where CustodianPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _custodian.CustodianPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _custodian.ApprovedUsersID);
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

        public void Custodian_Reject(Custodian _custodian)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Custodian set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where CustodianPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _custodian.CustodianPK);
                        cmd.Parameters.AddWithValue("@historyPK", _custodian.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _custodian.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Custodian set status= 2,lastupdate=@lastupdate where CustodianPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _custodian.CustodianPK);
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

        public void Custodian_Void(Custodian _custodian)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Custodian set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate " +
                            "where CustodianPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _custodian.CustodianPK);
                        cmd.Parameters.AddWithValue("@historyPK", _custodian.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _custodian.VoidUsersID);
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

        public List<CustodianCombo> Custodian_Combo()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CustodianCombo> L_Custodian = new List<CustodianCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  CustodianPK,ID + ' - ' + Name as ID, Name FROM [Custodian]  where status = 2  order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CustodianCombo M_Custodian = new CustodianCombo();
                                    M_Custodian.CustodianPK = Convert.ToInt32(dr["CustodianPK"]);
                                    M_Custodian.ID = Convert.ToString(dr["ID"]);
                                    M_Custodian.Name = Convert.ToString(dr["Name"]);
                                    L_Custodian.Add(M_Custodian);
                                }
                            }
                            return L_Custodian;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<CustodianCombo> Custodian_ComboChildOnly()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CustodianCombo> L_Custodian = new List<CustodianCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  CustodianPK,ID + ' - ' + Name as ID, Name FROM [Custodian]  where status = 2 and Groups = 0 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CustodianCombo M_Custodian = new CustodianCombo();
                                    M_Custodian.CustodianPK = Convert.ToInt32(dr["CustodianPK"]);
                                    M_Custodian.ID = Convert.ToString(dr["ID"]);
                                    M_Custodian.Name = Convert.ToString(dr["Name"]);
                                    L_Custodian.Add(M_Custodian);
                                }
                            }
                            return L_Custodian;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<CustodianCombo> Custodian_ComboGroupsOnly()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CustodianCombo> L_Custodian = new List<CustodianCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  CustodianPK,ID + ' - ' + Name as ID, Name FROM [Custodian]  where status = 2 and Groups = 1 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CustodianCombo M_Custodian = new CustodianCombo();
                                    M_Custodian.CustodianPK = Convert.ToInt32(dr["CustodianPK"]);
                                    M_Custodian.ID = Convert.ToString(dr["ID"]);
                                    M_Custodian.Name = Convert.ToString(dr["Name"]);
                                    L_Custodian.Add(M_Custodian);
                                }
                            }
                            return L_Custodian;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public List<CustodianCombo> Custodian_ParentCombo()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CustodianCombo> L_Custodian = new List<CustodianCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  CustodianPK,ID + ' - ' + Name as ParentID, Name as ParentName FROM [Custodian]  where status in (1, 2) and Groups = 1 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CustodianCombo M_Custodian = new CustodianCombo();
                                    M_Custodian.CustodianPK = Convert.ToInt32(dr["CustodianPK"]);
                                    M_Custodian.ID = Convert.ToString(dr["ParentID"]);
                                    M_Custodian.Name = Convert.ToString(dr["ParentName"]);
                                    L_Custodian.Add(M_Custodian);
                                }
                            }
                            return L_Custodian;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public bool Custodian_UpdateParentAndDepth()
        {
            try
            {
                using (SqlConnection DbConSubQuery = new SqlConnection(Tools.conString))
                {
                    DbConSubQuery.Open();
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbConSubQuery.CreateCommand())
                        {
                            cmd.CommandText = "UPDATE Custodian SET " +
                                                    "Custodian.ParentPK1 = isnull(Custodian_1.CustodianPK,0), Custodian.ParentPK2 = isnull(Custodian_2.CustodianPK,0), " +
                                                    "Custodian.ParentPK3 = isnull(Custodian_3.CustodianPK,0), Custodian.ParentPK4 = isnull(Custodian_4.CustodianPK,0),Custodian.ParentPK5 = isnull(Custodian_5.CustodianPK,0) " +
                                                    "FROM Custodian " +
                                                    "LEFT JOIN Custodian AS Custodian_1 ON Custodian.ParentPK = Custodian_1.CustodianPK " +
                                                    "LEFT JOIN Custodian AS Custodian_2 ON Custodian_1.ParentPK = Custodian_2.CustodianPK " +
                                                    "LEFT JOIN Custodian AS Custodian_3 ON Custodian_2.ParentPK = Custodian_3.CustodianPK " +
                                                    "LEFT JOIN Custodian AS Custodian_4 ON Custodian_3.ParentPK = Custodian_4.CustodianPK " +
                                                    "LEFT JOIN Custodian AS Custodian_5 ON Custodian_4.ParentPK = Custodian_5.CustodianPK " +
                                                    " Where Custodian.Status = 2";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "Select CustodianPK From Custodian Where Status = 2 and Groups = 1";
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    try
                                    {
                                        while (dr.Read())
                                        {
                                            using (SqlCommand cmdSubQuery = DbCon.CreateCommand())
                                            {
                                                cmdSubQuery.CommandText = "Update Custodian set Depth = @Depth Where CustodianPK = @CustodianPK and Status = 2";
                                                cmdSubQuery.Parameters.AddWithValue("@Depth", GetCustodianDepth(Convert.ToInt32(dr["CustodianPK"])));
                                                cmdSubQuery.Parameters.AddWithValue("@CustodianPK", Convert.ToInt32(dr["CustodianPK"]));
                                                cmdSubQuery.ExecuteNonQuery();
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
                        return true;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private int GetCustodianDepth(int _custodianPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "DECLARE @Depth INT,@Depth1 INT, @Depth2 INT, @Depth3 INT, @Depth4 INT, @Depth5 INT, @Depth10 INT " +
                                          "SELECT @Depth1 = MAX(Custodian_1.ParentPK), @Depth2 = MAX(Custodian_2.ParentPK), " +
                                          "@Depth3 = MAX(Custodian_3.ParentPK),@Depth4 = MAX(Custodian_4.ParentPK),@Depth5 = MAX(Custodian_5.ParentPK), @Depth10 = MAX(Custodian_10.ParentPK) " +
                                          "FROM Custodian AS Custodian_10 RIGHT JOIN (Custodian AS Custodian_5 RIGHT JOIN (Custodian AS Custodian_4 RIGHT JOIN (Custodian AS Custodian_3 " +
                                          "RIGHT JOIN (Custodian AS Custodian_2 RIGHT JOIN (Custodian AS Custodian_1 " +
                                          "RIGHT JOIN Custodian ON Custodian_1.ParentPK = Custodian.CustodianPK) " +
                                          "ON Custodian_2.ParentPK = Custodian_1.CustodianPK) ON Custodian_3.ParentPK = Custodian_2.CustodianPK) " +
                                          "ON Custodian_4.ParentPK = Custodian_3.CustodianPK) ON Custodian_5.ParentPK = Custodian_4.CustodianPK)  ON Custodian_10.ParentPK = Custodian_5.CustodianPK   " +
                                          "WHERE Custodian.CustodianPK = @CustodianPK and Custodian.Status = 2 " +
                                          "IF @Depth1 IS NULL " +
                                          "SET @Depth = 0  " +
                                          "ELSE IF @Depth2 IS NULL " +
                                          "SET @Depth = 1 " +
                                          "ELSE IF @Depth3 IS NULL " +
                                          "SET @Depth = 2 " +
                                          "ELSE IF @Depth4 IS NULL " +
                                          "SET @Depth = 3  " +
                                          "ELSE IF @Depth5 IS NULL " +
                                          "SET @Depth = 4  " +
                                          "ELSE IF @Depth10 IS NULL " +
                                          "SET @Depth = 5  " +
                                          "ELSE " +
                                          "SET @Depth = 0 " +
                                          "select @depth depth";
                        cmd.Parameters.AddWithValue("@CustodianPK", _custodianPK);
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