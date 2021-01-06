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
    public class SegmentClassReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[SegmentClass] " +
                            "([SegmentClassPK],[HistoryPK],[Status],[Name],[ParentCategoryPK],";
        string _paramaterCommand = "@Name,@ParentCategoryPK,";

        //2
        private SegmentClass setSegmentClass(SqlDataReader dr)
        {
            SegmentClass M_SegmentClass = new SegmentClass();
            M_SegmentClass.SegmentClassPK = Convert.ToInt32(dr["SegmentClassPK"]);
            M_SegmentClass.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_SegmentClass.Status = Convert.ToInt32(dr["Status"]);
            M_SegmentClass.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_SegmentClass.Notes = Convert.ToString(dr["Notes"]);
            M_SegmentClass.Name = dr["Name"].ToString();
            M_SegmentClass.ParentCategoryPK = Convert.ToString(dr["ParentCategoryPK"]);
            M_SegmentClass.ParentCategoryDesc = Convert.ToString(dr["ParentCategoryDesc"]);
            M_SegmentClass.EntryUsersID = dr["EntryUsersID"].ToString();
            M_SegmentClass.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_SegmentClass.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_SegmentClass.VoidUsersID = dr["VoidUsersID"].ToString();
            M_SegmentClass.EntryTime = dr["EntryTime"].ToString();
            M_SegmentClass.UpdateTime = dr["UpdateTime"].ToString();
            M_SegmentClass.ApprovedTime = dr["ApprovedTime"].ToString();
            M_SegmentClass.VoidTime = dr["VoidTime"].ToString();
            M_SegmentClass.DBUserID = dr["DBUserID"].ToString();
            M_SegmentClass.DBTerminalID = dr["DBTerminalID"].ToString();
            M_SegmentClass.LastUpdate = dr["LastUpdate"].ToString();
            M_SegmentClass.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_SegmentClass;
        }

        public List<SegmentClass> SegmentClass_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SegmentClass> L_SegmentClass = new List<SegmentClass>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when B.status=1 then 'PENDING' else Case When B.status = 2 then 'APPROVED' else Case when B.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne ParentCategoryDesc,* from SegmentClass  B  
                            left join MasterValue MV on B.ParentCategoryPK=MV.Code and MV.ID ='InternalCategoryType' 
                            where B.status = @status order by SegmentClassPK ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when B.status=1 then 'PENDING' else Case When B.status = 2 then 'APPROVED' else Case when B.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne ParentCategoryDesc,* from SegmentClass  B  
                            left join MasterValue MV on B.ParentCategoryPK=MV.Code and MV.ID ='InternalCategoryType' 
                            where B.status = 3 order by SegmentClassPK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_SegmentClass.Add(setSegmentClass(dr));
                                }
                            }
                            return L_SegmentClass;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int SegmentClass_Add(SegmentClass _SegmentClass, bool _havePrivillege)
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
                                 "Select isnull(max(SegmentClassPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From SegmentClass";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _SegmentClass.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(SegmentClassPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From SegmentClass";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Name", _SegmentClass.Name);
                        cmd.Parameters.AddWithValue("@ParentCategoryPK", _SegmentClass.ParentCategoryPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _SegmentClass.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "SegmentClass");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int SegmentClass_Update(SegmentClass _SegmentClass, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_SegmentClass.SegmentClassPK, _SegmentClass.HistoryPK, "SegmentClass");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update SegmentClass set status=2, Notes=@Notes,Name=@Name, ParentCategoryPK=@ParentCategoryPK,
                                ApprovedUsersID=@ApprovedUsersID,  
                                ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate  
                                where SegmentClassPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _SegmentClass.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _SegmentClass.SegmentClassPK);
                            cmd.Parameters.AddWithValue("@Notes", _SegmentClass.Notes);
                            cmd.Parameters.AddWithValue("@Name", _SegmentClass.Name);
                            cmd.Parameters.AddWithValue("@ParentCategoryPK", _SegmentClass.ParentCategoryPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _SegmentClass.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _SegmentClass.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update SegmentClass set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where SegmentClassPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _SegmentClass.SegmentClassPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _SegmentClass.EntryUsersID);
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
                                cmd.CommandText = @"Update SegmentClass set Notes=@Notes,Name=@Name, ParentCategoryPK=@ParentCategoryPK,
                                    UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate  
                                    where SegmentClassPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _SegmentClass.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _SegmentClass.SegmentClassPK);
                                cmd.Parameters.AddWithValue("@Notes", _SegmentClass.Notes);
                                cmd.Parameters.AddWithValue("@Name", _SegmentClass.Name);
                                cmd.Parameters.AddWithValue("@ParentCategoryPK", _SegmentClass.ParentCategoryPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _SegmentClass.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_SegmentClass.SegmentClassPK, "SegmentClass");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From SegmentClass where SegmentClassPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _SegmentClass.SegmentClassPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _SegmentClass.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Name", _SegmentClass.Name);
                                cmd.Parameters.AddWithValue("@ParentCategoryPK", _SegmentClass.ParentCategoryPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _SegmentClass.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update SegmentClass set status= 4,Notes=@Notes," +
                                    "LastUpdate=LastUpdate where SegmentClassPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _SegmentClass.Notes);
                                cmd.Parameters.AddWithValue("@PK", _SegmentClass.SegmentClassPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _SegmentClass.HistoryPK);
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

        public void SegmentClass_Approved(SegmentClass _SegmentClass)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SegmentClass set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where SegmentClassPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SegmentClass.SegmentClassPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SegmentClass.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _SegmentClass.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update SegmentClass set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where SegmentClassPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _SegmentClass.SegmentClassPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SegmentClass.ApprovedUsersID);
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

        public void SegmentClass_Reject(SegmentClass _SegmentClass)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SegmentClass set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where SegmentClassPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SegmentClass.SegmentClassPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SegmentClass.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SegmentClass.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update SegmentClass set status= 2,LastUpdate=@LastUpdate where SegmentClassPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _SegmentClass.SegmentClassPK);
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

        public void SegmentClass_Void(SegmentClass _SegmentClass)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SegmentClass set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where SegmentClassPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SegmentClass.SegmentClassPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SegmentClass.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SegmentClass.VoidUsersID);
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

        public List<SegmentClassCombo> SegmentClass_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SegmentClassCombo> L_SegmentClass = new List<SegmentClassCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  SegmentClassPK, Name FROM [SegmentClass]  where status = 2 order by Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    SegmentClassCombo M_SegmentClass = new SegmentClassCombo();
                                    M_SegmentClass.SegmentClassPK = Convert.ToInt32(dr["SegmentClassPK"]);
                                    M_SegmentClass.Name = Convert.ToString(dr["Name"]);
                                    L_SegmentClass.Add(M_SegmentClass);
                                }

                            }
                            return L_SegmentClass;
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