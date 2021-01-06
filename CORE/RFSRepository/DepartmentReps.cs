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
    public class DepartmentReps
    {
        Host _host = new Host();
        
        //1
        string _insertCommand = "INSERT INTO [dbo].[Department] " +
                            "([DepartmentPK],[HistoryPK],[Status],[ID],[Name],[Groups],[Levels],[ParentPK],"+
                            "[ParentPK1],[ParentPK2],[ParentPK3],[ParentPK4],[ParentPK5],[ParentPK6],[ParentPK7],[ParentPK8],[ParentPK9],[BitShowinFinance],";
        string _paramaterCommand = "@ID,@Name,@Groups,@Levels,@ParentPK,@ParentPK1,@ParentPK2,@ParentPK3,@ParentPK4,@ParentPK5,@ParentPK6,@ParentPK7,@ParentPK8,@ParentPK9,@BitShowinFinance,";

        //2
        private Department setDepartment(SqlDataReader dr)
        {
            Department M_Department = new Department();
            M_Department.DepartmentPK = Convert.ToInt32(dr["DepartmentPK"]);
            M_Department.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Department.Status = Convert.ToInt32(dr["Status"]);
            M_Department.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Department.Notes = Convert.ToString(dr["Notes"]);
            M_Department.ID = dr["ID"].ToString();
            M_Department.Name = dr["Name"].ToString();
            M_Department.Groups = Convert.ToBoolean(dr["Groups"]);
            M_Department.Levels = Convert.ToInt32(dr["Levels"]);
            M_Department.ParentPK = Convert.ToInt32(dr["ParentPK"]);
            M_Department.ParentID = dr["ParentID"].ToString();
            M_Department.ParentName = dr["ParentName"].ToString();
            M_Department.ParentPK1 = Convert.ToInt32(dr["ParentPK1"]);
            M_Department.ParentPK2 = Convert.ToInt32(dr["ParentPK2"]);
            M_Department.ParentPK3 = Convert.ToInt32(dr["ParentPK3"]);
            M_Department.ParentPK4 = Convert.ToInt32(dr["ParentPK4"]);
            M_Department.ParentPK5 = Convert.ToInt32(dr["ParentPK5"]);
            M_Department.ParentPK6 = Convert.ToInt32(dr["ParentPK6"]);
            M_Department.ParentPK7 = Convert.ToInt32(dr["ParentPK7"]);
            M_Department.ParentPK8 = Convert.ToInt32(dr["ParentPK8"]);
            M_Department.ParentPK9 = Convert.ToInt32(dr["ParentPK9"]);
            M_Department.Depth = Convert.ToInt32(dr["Depth"]);
            M_Department.BitShowinFinance = Convert.ToBoolean(dr["BitShowinFinance"]);
            M_Department.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Department.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Department.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Department.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Department.EntryTime = dr["EntryTime"].ToString();
            M_Department.UpdateTime = dr["UpdateTime"].ToString();
            M_Department.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Department.VoidTime = dr["VoidTime"].ToString();
            M_Department.DBUserID = dr["DBUserID"].ToString();
            M_Department.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Department.LastUpdate = dr["LastUpdate"].ToString();
            M_Department.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_Department;
        }

        public List<Department> Department_Select(int _status)
        {
             try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Department> L_Department = new List<Department>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when D.status=1 then 'PENDING' else Case When D.status = 2 then 'APPROVED' else Case when D.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,ZD.ID ParentID,ZD.Name ParentName, D.* from Department D left join " +
                                              " Department ZD on D.ParentPK = ZD.DepartmentPK and ZD.status in (1,2) " +
                                              " where D.status = @status order by D.DepartmentPK";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when D.status=1 then 'PENDING' else Case When D.status = 2 then 'APPROVED' else Case when D.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,ZD.ID ParentID,ZD.Name ParentName, D.* from Department D left join " +
                                              " Department ZD on D.ParentPK = ZD.DepartmentPK and ZD.status in (1,2) " +
                                              " order by D.DepartmentPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Department.Add(setDepartment(dr));
                                }
                            }
                            return L_Department;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int Department_Add(Department _department, bool _havePrivillege)
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
                                 "Select isnull(max(DepartmentPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate From Department";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _department.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(DepartmentPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate From Department";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _department.ID);
                        cmd.Parameters.AddWithValue("@Name", _department.Name);
                        cmd.Parameters.AddWithValue("@Groups", _department.Groups);
                        cmd.Parameters.AddWithValue("@Levels", _department.Levels);
                        cmd.Parameters.AddWithValue("@ParentPK", _department.ParentPK);
                        cmd.Parameters.AddWithValue("@ParentPK1", _department.ParentPK1);
                        cmd.Parameters.AddWithValue("@ParentPK2", _department.ParentPK2);
                        cmd.Parameters.AddWithValue("@ParentPK3", _department.ParentPK3);
                        cmd.Parameters.AddWithValue("@ParentPK4", _department.ParentPK4);
                        cmd.Parameters.AddWithValue("@ParentPK5", _department.ParentPK5);
                        cmd.Parameters.AddWithValue("@ParentPK6", _department.ParentPK6);
                        cmd.Parameters.AddWithValue("@ParentPK7", _department.ParentPK7);
                        cmd.Parameters.AddWithValue("@ParentPK8", _department.ParentPK8);
                        cmd.Parameters.AddWithValue("@ParentPK9", _department.ParentPK9);
                        cmd.Parameters.AddWithValue("@BitShowinFinance", _department.BitShowinFinance);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _department.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "Department");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int Department_Update(Department _department, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_department.DepartmentPK, _department.HistoryPK, "department");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Department set status=2, Notes=@Notes,ID=@ID,Name=@Name,Groups=@Groups," +
                                "Levels=@Levels,ParentPK=@ParentPK,ParentPK1=@ParentPK1,ParentPK2=@ParentPK2,ParentPK3=@ParentPK3,ParentPK4=@ParentPK4," +
                                "ParentPK5=@ParentPK5,ParentPK6=@ParentPK6,ParentPK7=@ParentPK7,ParentPK8=@ParentPK8,ParentPK9=@ParentPK9,BitShowinFinance=@BitShowinFinance," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where DepartmentPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _department.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _department.DepartmentPK);
                            cmd.Parameters.AddWithValue("@Notes", _department.Notes);
                            cmd.Parameters.AddWithValue("@ID", _department.ID);
                            cmd.Parameters.AddWithValue("@Name", _department.Name);
                            cmd.Parameters.AddWithValue("@Groups", _department.Groups);
                            cmd.Parameters.AddWithValue("@Levels", _department.Levels);
                            cmd.Parameters.AddWithValue("@ParentPK", _department.ParentPK);
                            cmd.Parameters.AddWithValue("@ParentPK1", _department.ParentPK1);
                            cmd.Parameters.AddWithValue("@ParentPK2", _department.ParentPK2);
                            cmd.Parameters.AddWithValue("@ParentPK3", _department.ParentPK3);
                            cmd.Parameters.AddWithValue("@ParentPK4", _department.ParentPK4);
                            cmd.Parameters.AddWithValue("@ParentPK5", _department.ParentPK5);
                            cmd.Parameters.AddWithValue("@ParentPK6", _department.ParentPK6);
                            cmd.Parameters.AddWithValue("@ParentPK7", _department.ParentPK7);
                            cmd.Parameters.AddWithValue("@ParentPK8", _department.ParentPK8);
                            cmd.Parameters.AddWithValue("@ParentPK9", _department.ParentPK9);
                            cmd.Parameters.AddWithValue("@BitShowinFinance", _department.BitShowinFinance);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _department.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _department.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Department set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate = @lastupdate where DepartmentPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _department.DepartmentPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _department.EntryUsersID);
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
                                cmd.CommandText = "Update Department set Notes=@Notes,ID=@ID,Name=@Name,Groups=@Groups," +
                                    "Levels=@Levels,ParentPK=@ParentPK,ParentPK1=@ParentPK1,ParentPK2=@ParentPK2,ParentPK3=@ParentPK3,ParentPK4=@ParentPK4," +
                                    "ParentPK5=@ParentPK5,ParentPK6=@ParentPK6,ParentPK7=@ParentPK7,ParentPK8=@ParentPK8,ParentPK9=@ParentPK9,BitShowinFinance=@BitShowinFinance," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where DepartmentPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _department.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _department.DepartmentPK);
                                cmd.Parameters.AddWithValue("@Notes", _department.Notes);
                                cmd.Parameters.AddWithValue("@ID", _department.ID);
                                cmd.Parameters.AddWithValue("@Name", _department.Name);
                                cmd.Parameters.AddWithValue("@Groups", _department.Groups);
                                cmd.Parameters.AddWithValue("@Levels", _department.Levels);
                                cmd.Parameters.AddWithValue("@ParentPK", _department.ParentPK);
                                cmd.Parameters.AddWithValue("@ParentPK1", _department.ParentPK1);
                                cmd.Parameters.AddWithValue("@ParentPK2", _department.ParentPK2);
                                cmd.Parameters.AddWithValue("@ParentPK3", _department.ParentPK3);
                                cmd.Parameters.AddWithValue("@ParentPK4", _department.ParentPK4);
                                cmd.Parameters.AddWithValue("@ParentPK5", _department.ParentPK5);
                                cmd.Parameters.AddWithValue("@ParentPK6", _department.ParentPK6);
                                cmd.Parameters.AddWithValue("@ParentPK7", _department.ParentPK7);
                                cmd.Parameters.AddWithValue("@ParentPK8", _department.ParentPK8);
                                cmd.Parameters.AddWithValue("@ParentPK9", _department.ParentPK9);
                                cmd.Parameters.AddWithValue("@BitShowinFinance", _department.BitShowinFinance);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _department.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_department.DepartmentPK, "Department");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Department where DepartmentPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _department.DepartmentPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _department.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _department.ID);
                                cmd.Parameters.AddWithValue("@Name", _department.Name);
                                cmd.Parameters.AddWithValue("@Groups", _department.Groups);
                                cmd.Parameters.AddWithValue("@Levels", _department.Levels);
                                cmd.Parameters.AddWithValue("@ParentPK", _department.ParentPK);
                                cmd.Parameters.AddWithValue("@ParentPK1", _department.ParentPK1);
                                cmd.Parameters.AddWithValue("@ParentPK2", _department.ParentPK2);
                                cmd.Parameters.AddWithValue("@ParentPK3", _department.ParentPK3);
                                cmd.Parameters.AddWithValue("@ParentPK4", _department.ParentPK4);
                                cmd.Parameters.AddWithValue("@ParentPK5", _department.ParentPK5);
                                cmd.Parameters.AddWithValue("@ParentPK6", _department.ParentPK6);
                                cmd.Parameters.AddWithValue("@ParentPK7", _department.ParentPK7);
                                cmd.Parameters.AddWithValue("@ParentPK8", _department.ParentPK8);
                                cmd.Parameters.AddWithValue("@ParentPK9", _department.ParentPK9);
                                cmd.Parameters.AddWithValue("@BitShowinFinance", _department.BitShowinFinance);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _department.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Department set status= 4,Notes=@Notes,"+
                                    "lastupdate=@lastupdate where DepartmentPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _department.Notes);
                                cmd.Parameters.AddWithValue("@PK", _department.DepartmentPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _department.HistoryPK);
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

        public void Department_Approved(Department _department)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Department set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime, lastupdate=@lastupdate " +
                            "where DepartmentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _department.DepartmentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _department.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _department.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Department set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate = @lastupdate where DepartmentPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _department.DepartmentPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _department.ApprovedUsersID);
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

        public void Department_Reject(Department _department)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Department set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where DepartmentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _department.DepartmentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _department.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _department.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Department set status= 2,lastupdate=@lastupdate where DepartmentPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _department.DepartmentPK);
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

        public void Department_Void(Department _department)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Department set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where DepartmentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _department.DepartmentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _department.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _department.VoidUsersID);
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

        public List<DepartmentCombo> Department_ComboRpt()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<DepartmentCombo> L_department = new List<DepartmentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  DepartmentPK,ID + ' - ' + Name as ID, Name FROM [Department]  where status = 2 union all select 0,'All', '' order by DepartmentPK,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    DepartmentCombo M_department = new DepartmentCombo();
                                    M_department.DepartmentPK = Convert.ToInt32(dr["DepartmentPK"]);
                                    M_department.ID = Convert.ToString(dr["ID"]);
                                    M_department.Name = Convert.ToString(dr["Name"]);
                                    L_department.Add(M_department);
                                }
                            }
                            return L_department;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<DepartmentCombo> Department_Combo()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<DepartmentCombo> L_department = new List<DepartmentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  DepartmentPK,ID + ' - ' + Name as [ID], Name FROM [Department]   where status = 2  order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    DepartmentCombo M_department = new DepartmentCombo();
                                    M_department.DepartmentPK = Convert.ToInt32(dr["DepartmentPK"]);
                                    M_department.ID = Convert.ToString(dr["ID"]);
                                    M_department.Name = Convert.ToString(dr["Name"]);
                                    L_department.Add(M_department);
                                }
                            }
                            return L_department;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<DepartmentCombo> Department_ComboShowFinanceOnly()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<DepartmentCombo> L_department = new List<DepartmentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  DepartmentPK,ID + ' - ' + Name as [ID], Name FROM [Department]   where status = 2 and BitShowInFinance = 1  order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    DepartmentCombo M_department = new DepartmentCombo();
                                    M_department.DepartmentPK = Convert.ToInt32(dr["DepartmentPK"]);
                                    M_department.ID = Convert.ToString(dr["ID"]);
                                    M_department.Name = Convert.ToString(dr["Name"]);
                                    L_department.Add(M_department);
                                }
                            }
                            return L_department;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<DepartmentCombo> Department_ComboGroupOnly()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<DepartmentCombo> L_department = new List<DepartmentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  DepartmentPK,ID + ' - ' + Name as [ID], Name FROM [Department]  where Groups = 1 and status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    DepartmentCombo M_department = new DepartmentCombo();
                                    M_department.DepartmentPK = Convert.ToInt32(dr["DepartmentPK"]);
                                    M_department.ID = Convert.ToString(dr["ID"]);
                                    M_department.Name = Convert.ToString(dr["Name"]);
                                    L_department.Add(M_department);
                                }
                            }
                            return L_department;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<DepartmentCombo> Department_ComboChildOnly()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<DepartmentCombo> L_department = new List<DepartmentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  DepartmentPK,ID + ' - ' + Name as [ID], Name FROM [Department]  where Groups = 0 and status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    DepartmentCombo M_department = new DepartmentCombo();
                                    M_department.DepartmentPK = Convert.ToInt32(dr["DepartmentPK"]);
                                    M_department.ID = Convert.ToString(dr["ID"]);
                                    M_department.Name = Convert.ToString(dr["Name"]);
                                    L_department.Add(M_department);
                                }
                            }
                            return L_department;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public bool Department_UpdateParentAndDepth()
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE Department SET " +
                                                "Department.ParentPK1 = isnull(Department_1.DepartmentPK,0), Department.ParentPK2 = isnull(Department_2.DepartmentPK,0), " +
                                                "Department.ParentPK3 = isnull(Department_3.DepartmentPK,0), Department.ParentPK4 = isnull(Department_4.DepartmentPK,0), " +
                                                "Department.ParentPK5 = isnull(Department_5.DepartmentPK,0), Department.ParentPK6 = isnull(Department_6.DepartmentPK,0), " +
                                                "Department.ParentPK7 = isnull(Department_7.DepartmentPK,0), Department.ParentPK8 = isnull(Department_8.DepartmentPK,0), " +
                                                "Department.ParentPK9 = isnull(Department_9.DepartmentPK,0)  " +
                                                "FROM Department " +
                                                "LEFT JOIN Department AS Department_1 ON Department.ParentPK = Department_1.DepartmentPK " +
                                                "LEFT JOIN Department AS Department_2 ON Department_1.ParentPK = Department_2.DepartmentPK " +
                                                "LEFT JOIN Department AS Department_3 ON Department_2.ParentPK = Department_3.DepartmentPK " +
                                                "LEFT JOIN Department AS Department_4 ON Department_3.ParentPK = Department_4.DepartmentPK " +
                                                "LEFT JOIN Department AS Department_5 ON Department_4.ParentPK = Department_5.DepartmentPK " +
                                                "LEFT JOIN Department AS Department_6 ON Department_5.ParentPK = Department_6.DepartmentPK " +
                                                "LEFT JOIN Department AS Department_7 ON Department_6.ParentPK = Department_7.DepartmentPK " +
                                                "LEFT JOIN Department AS Department_8 ON Department_7.ParentPK = Department_8.DepartmentPK " +
                                                "LEFT JOIN Department AS Department_9 ON Department_8.ParentPK = Department_9.DepartmentPK Where Department.Status = 2 " ;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Select DepartmentPK From Department Where Status = 2 and Groups = 1";
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
                                                cmdSubQuery.CommandText = "Update Department set Depth = @Depth, lastupdate=@lastupdate Where DepartmentPK = @DepartmentPK and Status = 2";
                                                cmdSubQuery.Parameters.AddWithValue("@Depth", GetDepartmentDepth(Convert.ToInt32(dr["DepartmentPK"])));
                                                cmdSubQuery.Parameters.AddWithValue("@DepartmentPK", Convert.ToInt32(dr["DepartmentPK"]));
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

        private int GetDepartmentDepth(int _departmentPK)
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
                                          "SELECT @Depth1 = MAX(Department_1.ParentPK), @Depth2 = MAX(Department_2.ParentPK), " +
                                          "@Depth3 = MAX(Department_3.ParentPK), @Depth4 = MAX(Department_4.ParentPK), " +
                                          "@Depth5 = MAX(Department_5.ParentPK), @Depth6 = MAX(Department_6.ParentPK), " +
                                          "@Depth7 = MAX(Department_7.ParentPK), @Depth8 = MAX(Department_8.ParentPK), " +
                                          "@Depth9 = MAX(Department_9.ParentPK), @Depth10 = MAX(Department_10.ParentPK) " +
                                          "FROM Department AS Department_10 RIGHT JOIN (Department AS Department_9 " +
                                          "RIGHT JOIN (Department AS Department_8 RIGHT JOIN (Department AS Department_7 " +
                                          "RIGHT JOIN (Department AS Department_6 RIGHT JOIN (Department AS Department_5 " +
                                          "RIGHT JOIN (Department AS Department_4 RIGHT JOIN (Department AS Department_3 " +
                                          "RIGHT JOIN (Department AS Department_2 RIGHT JOIN (Department AS Department_1 " +
                                          "RIGHT JOIN Department ON Department_1.ParentPK = Department.DepartmentPK) " +
                                          "ON Department_2.ParentPK = Department_1.DepartmentPK) ON Department_3.ParentPK = Department_2.DepartmentPK) " +
                                          "ON Department_4.ParentPK = Department_3.DepartmentPK) ON Department_5.ParentPK = Department_4.DepartmentPK) " +
                                          "ON Department_6.ParentPK = Department_5.DepartmentPK) ON Department_7.ParentPK = Department_6.DepartmentPK) " +
                                          "ON Department_8.ParentPK = Department_7.DepartmentPK) ON Department_9.ParentPK = Department_8.DepartmentPK) " +
                                          "ON Department_10.ParentPK = Department_9.DepartmentPK  " +
                                          "WHERE Department.DepartmentPK = @DepartmentPK and Department.Status = 2 " +
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
                        cmd.Parameters.AddWithValue("@DepartmentPK", _departmentPK);
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