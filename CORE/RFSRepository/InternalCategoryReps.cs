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
    public class InternalCategoryReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[InternalCategory] " +
                            "([InternalCategoryPK],[HistoryPK],[Status],[ID],[Name],[Groups],[Levels],[ParentPK]," +
                            "[ParentPK1],[ParentPK2],[ParentPK3],[ParentPK4],[ParentPK5],[ParentPK6],[ParentPK7],[ParentPK8],[ParentPK9],";
        string _paramaterCommand = "@ID,@Name,@Groups,@Levels,@ParentPK,@ParentPK1,@ParentPK2,@ParentPK3,@ParentPK4,@ParentPK5,@ParentPK6,@ParentPK7,@ParentPK8,@ParentPK9,";

        //2
        private InternalCategory setInternalCategory(SqlDataReader dr)
        {
            InternalCategory M_InternalCategory = new InternalCategory();
            M_InternalCategory.InternalCategoryPK = Convert.ToInt32(dr["InternalCategoryPK"]);
            M_InternalCategory.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_InternalCategory.Status = Convert.ToInt32(dr["Status"]);
            M_InternalCategory.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_InternalCategory.Notes = Convert.ToString(dr["Notes"]);
            M_InternalCategory.ID = dr["ID"].ToString();
            M_InternalCategory.Name = dr["Name"].ToString();
            M_InternalCategory.Groups = Convert.ToBoolean(dr["Groups"]);
            M_InternalCategory.Levels = Convert.ToInt32(dr["Levels"]);
            M_InternalCategory.ParentPK = Convert.ToInt32(dr["ParentPK"]);
            M_InternalCategory.ParentID = dr["ParentID"].ToString();
            M_InternalCategory.ParentName = dr["ParentName"].ToString();
            M_InternalCategory.ParentPK1 = Convert.ToInt32(dr["ParentPK1"]);
            M_InternalCategory.ParentPK2 = Convert.ToInt32(dr["ParentPK2"]);
            M_InternalCategory.ParentPK3 = Convert.ToInt32(dr["ParentPK3"]);
            M_InternalCategory.ParentPK4 = Convert.ToInt32(dr["ParentPK4"]);
            M_InternalCategory.ParentPK5 = Convert.ToInt32(dr["ParentPK5"]);
            M_InternalCategory.ParentPK6 = Convert.ToInt32(dr["ParentPK6"]);
            M_InternalCategory.ParentPK7 = Convert.ToInt32(dr["ParentPK7"]);
            M_InternalCategory.ParentPK8 = Convert.ToInt32(dr["ParentPK8"]);
            M_InternalCategory.ParentPK9 = Convert.ToInt32(dr["ParentPK9"]);
            M_InternalCategory.Depth = Convert.ToInt32(dr["Depth"]);
            M_InternalCategory.EntryUsersID = dr["EntryUsersID"].ToString();
            M_InternalCategory.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_InternalCategory.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_InternalCategory.VoidUsersID = dr["VoidUsersID"].ToString();
            M_InternalCategory.EntryTime = dr["EntryTime"].ToString();
            M_InternalCategory.UpdateTime = dr["UpdateTime"].ToString();
            M_InternalCategory.ApprovedTime = dr["ApprovedTime"].ToString();
            M_InternalCategory.VoidTime = dr["VoidTime"].ToString();
            M_InternalCategory.DBUserID = dr["DBUserID"].ToString();
            M_InternalCategory.DBTerminalID = dr["DBTerminalID"].ToString();
            M_InternalCategory.LastUpdate = dr["LastUpdate"].ToString();
            M_InternalCategory.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_InternalCategory;
        }

        public List<InternalCategory> InternalCategory_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InternalCategory> L_InternalCategory = new List<InternalCategory>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when B.status=1 then 'PENDING' else Case When B.status = 2 then 'APPROVED' else Case when B.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,ZD.ID ParentID,ZD.Name ParentName,* from InternalCategory  B  left join" +
                            " InternalCategory ZD on B.ParentPK = ZD.InternalCategoryPK and ZD.status in (1,2) " +
                            " where B.status = @status order by B.InternalCategoryPK ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when B.status=1 then 'PENDING' else Case When B.status = 2 then 'APPROVED' else Case when B.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,ZD.ID ParentID,ZD.Name ParentName,* from InternalCategory  B  left join" +
                            " InternalCategory ZD on B.ParentPK = ZD.InternalCategoryPK and ZD.status in (1,2) " +
                            " order by B.InternalCategoryPK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_InternalCategory.Add(setInternalCategory(dr));
                                }
                            }
                            return L_InternalCategory;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int InternalCategory_Add(InternalCategory _internalCategory, bool _havePrivillege)
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
                                 "Select isnull(max(InternalCategoryPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From InternalCategory";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _internalCategory.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(InternalCategoryPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From InternalCategory";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _internalCategory.ID);
                        cmd.Parameters.AddWithValue("@Name", _internalCategory.Name);
                        cmd.Parameters.AddWithValue("@Groups", _internalCategory.Groups);
                        cmd.Parameters.AddWithValue("@Levels", _internalCategory.Levels);
                        cmd.Parameters.AddWithValue("@ParentPK", _internalCategory.ParentPK);
                        cmd.Parameters.AddWithValue("@ParentPK1", _internalCategory.ParentPK1);
                        cmd.Parameters.AddWithValue("@ParentPK2", _internalCategory.ParentPK2);
                        cmd.Parameters.AddWithValue("@ParentPK3", _internalCategory.ParentPK3);
                        cmd.Parameters.AddWithValue("@ParentPK4", _internalCategory.ParentPK4);
                        cmd.Parameters.AddWithValue("@ParentPK5", _internalCategory.ParentPK5);
                        cmd.Parameters.AddWithValue("@ParentPK6", _internalCategory.ParentPK6);
                        cmd.Parameters.AddWithValue("@ParentPK7", _internalCategory.ParentPK7);
                        cmd.Parameters.AddWithValue("@ParentPK8", _internalCategory.ParentPK8);
                        cmd.Parameters.AddWithValue("@ParentPK9", _internalCategory.ParentPK9);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _internalCategory.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "InternalCategory");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int InternalCategory_Update(InternalCategory _internalCategory, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_internalCategory.InternalCategoryPK, _internalCategory.HistoryPK, "InternalCategory");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update InternalCategory set status=2, Notes=@Notes, ID=@ID, Name=@Name,Groups=@Groups," +
                                "Levels=@Levels,ParentPK=@ParentPK,ParentPK1=@ParentPK1,ParentPK2=@ParentPK2,ParentPK3=@ParentPK3,ParentPK4=@ParentPK4," +
                                "ParentPK5=@ParentPK5,ParentPK6=@ParentPK6,ParentPK7=@ParentPK7,ParentPK8=@ParentPK8,ParentPK9=@ParentPK9," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where InternalCategoryPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _internalCategory.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _internalCategory.InternalCategoryPK);
                            cmd.Parameters.AddWithValue("@Notes", _internalCategory.Notes);
                            cmd.Parameters.AddWithValue("@ID", _internalCategory.ID);
                            cmd.Parameters.AddWithValue("@Name", _internalCategory.Name);
                            cmd.Parameters.AddWithValue("@Groups", _internalCategory.Groups);
                            cmd.Parameters.AddWithValue("@Levels", _internalCategory.Levels);
                            cmd.Parameters.AddWithValue("@ParentPK", _internalCategory.ParentPK);
                            cmd.Parameters.AddWithValue("@ParentPK1", _internalCategory.ParentPK1);
                            cmd.Parameters.AddWithValue("@ParentPK2", _internalCategory.ParentPK2);
                            cmd.Parameters.AddWithValue("@ParentPK3", _internalCategory.ParentPK3);
                            cmd.Parameters.AddWithValue("@ParentPK4", _internalCategory.ParentPK4);
                            cmd.Parameters.AddWithValue("@ParentPK5", _internalCategory.ParentPK5);
                            cmd.Parameters.AddWithValue("@ParentPK6", _internalCategory.ParentPK6);
                            cmd.Parameters.AddWithValue("@ParentPK7", _internalCategory.ParentPK7);
                            cmd.Parameters.AddWithValue("@ParentPK8", _internalCategory.ParentPK8);
                            cmd.Parameters.AddWithValue("@ParentPK9", _internalCategory.ParentPK9);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _internalCategory.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _internalCategory.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update InternalCategory set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where InternalCategoryPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _internalCategory.InternalCategoryPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _internalCategory.EntryUsersID);
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
                                cmd.CommandText = "Update InternalCategory set Notes=@Notes,ID=@ID,Name=@Name,Groups=@Groups," +
                                "Levels=@Levels,ParentPK=@ParentPK,ParentPK1=@ParentPK1,ParentPK2=@ParentPK2,ParentPK3=@ParentPK3,ParentPK4=@ParentPK4," +
                                "ParentPK5=@ParentPK5,ParentPK6=@ParentPK6,ParentPK7=@ParentPK7,ParentPK8=@ParentPK8,ParentPK9=@ParentPK9," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where InternalCategoryPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _internalCategory.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _internalCategory.InternalCategoryPK);
                                cmd.Parameters.AddWithValue("@Notes", _internalCategory.Notes);
                                cmd.Parameters.AddWithValue("@ID", _internalCategory.ID);
                                cmd.Parameters.AddWithValue("@Name", _internalCategory.Name);
                                cmd.Parameters.AddWithValue("@Groups", _internalCategory.Groups);
                                cmd.Parameters.AddWithValue("@Levels", _internalCategory.Levels);
                                cmd.Parameters.AddWithValue("@ParentPK", _internalCategory.ParentPK);
                                cmd.Parameters.AddWithValue("@ParentPK1", _internalCategory.ParentPK1);
                                cmd.Parameters.AddWithValue("@ParentPK2", _internalCategory.ParentPK2);
                                cmd.Parameters.AddWithValue("@ParentPK3", _internalCategory.ParentPK3);
                                cmd.Parameters.AddWithValue("@ParentPK4", _internalCategory.ParentPK4);
                                cmd.Parameters.AddWithValue("@ParentPK5", _internalCategory.ParentPK5);
                                cmd.Parameters.AddWithValue("@ParentPK6", _internalCategory.ParentPK6);
                                cmd.Parameters.AddWithValue("@ParentPK7", _internalCategory.ParentPK7);
                                cmd.Parameters.AddWithValue("@ParentPK8", _internalCategory.ParentPK8);
                                cmd.Parameters.AddWithValue("@ParentPK9", _internalCategory.ParentPK9);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _internalCategory.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_internalCategory.InternalCategoryPK, "InternalCategory");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From InternalCategory where InternalCategoryPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _internalCategory.InternalCategoryPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _internalCategory.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _internalCategory.ID);
                                cmd.Parameters.AddWithValue("@Name", _internalCategory.Name);
                                cmd.Parameters.AddWithValue("@Groups", _internalCategory.Groups);
                                cmd.Parameters.AddWithValue("@Levels", _internalCategory.Levels);
                                cmd.Parameters.AddWithValue("@ParentPK", _internalCategory.ParentPK);
                                cmd.Parameters.AddWithValue("@ParentPK1", _internalCategory.ParentPK1);
                                cmd.Parameters.AddWithValue("@ParentPK2", _internalCategory.ParentPK2);
                                cmd.Parameters.AddWithValue("@ParentPK3", _internalCategory.ParentPK3);
                                cmd.Parameters.AddWithValue("@ParentPK4", _internalCategory.ParentPK4);
                                cmd.Parameters.AddWithValue("@ParentPK5", _internalCategory.ParentPK5);
                                cmd.Parameters.AddWithValue("@ParentPK6", _internalCategory.ParentPK6);
                                cmd.Parameters.AddWithValue("@ParentPK7", _internalCategory.ParentPK7);
                                cmd.Parameters.AddWithValue("@ParentPK8", _internalCategory.ParentPK8);
                                cmd.Parameters.AddWithValue("@ParentPK9", _internalCategory.ParentPK9);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _internalCategory.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update InternalCategory set status= 4,Notes=@Notes," +
                                    "LastUpdate=LastUpdate where InternalCategoryPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _internalCategory.Notes);
                                cmd.Parameters.AddWithValue("@PK", _internalCategory.InternalCategoryPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _internalCategory.HistoryPK);
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

        public void InternalCategory_Approved(InternalCategory _internalCategory)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update InternalCategory set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where InternalCategoryPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _internalCategory.InternalCategoryPK);
                        cmd.Parameters.AddWithValue("@historyPK", _internalCategory.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _internalCategory.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update InternalCategory set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where InternalCategoryPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _internalCategory.InternalCategoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _internalCategory.ApprovedUsersID);
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

        public void InternalCategory_Reject(InternalCategory _internalCategory)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update InternalCategory set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where InternalCategoryPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _internalCategory.InternalCategoryPK);
                        cmd.Parameters.AddWithValue("@historyPK", _internalCategory.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _internalCategory.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update InternalCategory set status= 2,LastUpdate=@LastUpdate where InternalCategoryPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _internalCategory.InternalCategoryPK);
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

        public void InternalCategory_Void(InternalCategory _internalCategory)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update InternalCategory set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where InternalCategoryPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _internalCategory.InternalCategoryPK);
                        cmd.Parameters.AddWithValue("@historyPK", _internalCategory.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _internalCategory.VoidUsersID);
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

        public List<InternalCategoryCombo> InternalCategory_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InternalCategoryCombo> L_InternalCategory = new List<InternalCategoryCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  InternalCategoryPK, Name FROM [InternalCategory]  where status = 2 order by Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InternalCategoryCombo M_InternalCategory = new InternalCategoryCombo();
                                    M_InternalCategory.InternalCategoryPK = Convert.ToInt32(dr["InternalCategoryPK"]);
                                    M_InternalCategory.Name = Convert.ToString(dr["Name"]);
                                    L_InternalCategory.Add(M_InternalCategory);
                                }

                            }
                            return L_InternalCategory;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }

        public List<InternalCategoryCombo> InternalCategory_ComboGroupOnly()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InternalCategoryCombo> L_internalCategory = new List<InternalCategoryCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  InternalCategoryPK,ID + ' - ' + Name as [ID], Name FROM [InternalCategory]  where Groups = 1 and status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InternalCategoryCombo M_internalCategory = new InternalCategoryCombo();
                                    M_internalCategory.InternalCategoryPK = Convert.ToInt32(dr["InternalCategoryPK"]);
                                    M_internalCategory.ID = Convert.ToString(dr["ID"]);
                                    M_internalCategory.Name = Convert.ToString(dr["Name"]);
                                    L_internalCategory.Add(M_internalCategory);
                                }
                            }
                            return L_internalCategory;
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