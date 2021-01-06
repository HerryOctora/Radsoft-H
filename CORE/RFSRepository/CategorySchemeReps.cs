using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class CategorySchemeReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[CategoryScheme] " +
                            "([CategorySchemePK],[HistoryPK],[Status],[Date],[ID],[Name],[MFeePercentFrom],[MFeePercentTo],";
        string _paramaterCommand = "@Date,@ID,@Name,@MFeePercentFrom,@MFeePercentTo,";

        //2
        private CategoryScheme setCategoryScheme(SqlDataReader dr)
        {
            CategoryScheme M_CategoryScheme = new CategoryScheme();
            M_CategoryScheme.CategorySchemePK = Convert.ToInt32(dr["CategorySchemePK"]);
            M_CategoryScheme.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_CategoryScheme.Status = Convert.ToInt32(dr["Status"]);
            M_CategoryScheme.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_CategoryScheme.Notes = Convert.ToString(dr["Notes"]);
            M_CategoryScheme.Date = dr["Date"].ToString();
            M_CategoryScheme.ID = Convert.ToString(dr["ID"]);
            M_CategoryScheme.Name = Convert.ToString(dr["Name"]);
            M_CategoryScheme.MFeePercentFrom = Convert.ToDecimal(dr["MFeePercentFrom"]);
            M_CategoryScheme.MFeePercentTo = Convert.ToDecimal(dr["MFeePercentTo"]);
            M_CategoryScheme.EntryUsersID = dr["EntryUsersID"].ToString();
            M_CategoryScheme.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_CategoryScheme.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_CategoryScheme.VoidUsersID = dr["VoidUsersID"].ToString();
            M_CategoryScheme.EntryTime = dr["EntryTime"].ToString();
            M_CategoryScheme.UpdateTime = dr["UpdateTime"].ToString();
            M_CategoryScheme.ApprovedTime = dr["ApprovedTime"].ToString();
            M_CategoryScheme.VoidTime = dr["VoidTime"].ToString();
            M_CategoryScheme.DBUserID = dr["DBUserID"].ToString();
            M_CategoryScheme.DBTerminalID = dr["DBTerminalID"].ToString();
            M_CategoryScheme.LastUpdate = dr["LastUpdate"].ToString();
            M_CategoryScheme.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_CategoryScheme;
        }

        public List<CategoryScheme> CategoryScheme_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CategoryScheme> L_CategoryScheme = new List<CategoryScheme>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, * from CategoryScheme 
                            where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, * from CategoryScheme ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_CategoryScheme.Add(setCategoryScheme(dr));
                                }
                            }
                            return L_CategoryScheme;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int CategoryScheme_Add(CategoryScheme _CategoryScheme, bool _havePrivillege)
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
                                 "Select isnull(max(CategorySchemePk),0) + 1,1,2," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from CategoryScheme";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _CategoryScheme.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(CategorySchemePk),0) + 1,1,2," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from CategoryScheme";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _CategoryScheme.Date);
                        cmd.Parameters.AddWithValue("@ID", _CategoryScheme.ID);
                        cmd.Parameters.AddWithValue("@Name", _CategoryScheme.Name);
                        cmd.Parameters.AddWithValue("@MFeePercentFrom", _CategoryScheme.MFeePercentFrom);
                        cmd.Parameters.AddWithValue("@MFeePercentTo", _CategoryScheme.MFeePercentTo);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _CategoryScheme.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "CategoryScheme");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int CategoryScheme_Update(CategoryScheme _CategoryScheme, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_CategoryScheme.CategorySchemePK, _CategoryScheme.HistoryPK, "CategoryScheme");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CategoryScheme set status=2, Notes=@Notes,Date=@Date,ID=@ID,Name=@Name,MFeePercentFrom=@MFeePercentFrom,MFeePercentTo=@MFeePercentTo," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where CategorySchemePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _CategoryScheme.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _CategoryScheme.CategorySchemePK);
                            cmd.Parameters.AddWithValue("@Notes", _CategoryScheme.Notes);
                            cmd.Parameters.AddWithValue("@Date", _CategoryScheme.Date);
                            cmd.Parameters.AddWithValue("@ID", _CategoryScheme.ID);
                            cmd.Parameters.AddWithValue("@Name", _CategoryScheme.Name);
                            cmd.Parameters.AddWithValue("@MFeePercentFrom", _CategoryScheme.MFeePercentFrom);
                            cmd.Parameters.AddWithValue("@MFeePercentTo", _CategoryScheme.MFeePercentTo);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _CategoryScheme.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _CategoryScheme.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CategoryScheme set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CategorySchemePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _CategoryScheme.CategorySchemePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _CategoryScheme.EntryUsersID);
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
                                cmd.CommandText = "Update CategoryScheme set Notes=@Notes,Date=@Date,ID=@ID,Name=@Name,MFeePercentFrom=@MFeePercentFrom,MFeePercentTo=@MFeePercentTo," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where CategorySchemePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _CategoryScheme.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _CategoryScheme.CategorySchemePK);
                                cmd.Parameters.AddWithValue("@Notes", _CategoryScheme.Notes);
                                cmd.Parameters.AddWithValue("@Date", _CategoryScheme.Date);
                                cmd.Parameters.AddWithValue("@ID", _CategoryScheme.ID);
                                cmd.Parameters.AddWithValue("@Name", _CategoryScheme.Name);
                                cmd.Parameters.AddWithValue("@MFeePercentFrom", _CategoryScheme.MFeePercentFrom);
                                cmd.Parameters.AddWithValue("@MFeePercentTo", _CategoryScheme.MFeePercentTo);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _CategoryScheme.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_CategoryScheme.CategorySchemePK, "CategoryScheme");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From CategoryScheme where CategorySchemePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _CategoryScheme.CategorySchemePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _CategoryScheme.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _CategoryScheme.Date);
                                cmd.Parameters.AddWithValue("@ID", _CategoryScheme.ID);
                                cmd.Parameters.AddWithValue("@Name", _CategoryScheme.Name);
                                cmd.Parameters.AddWithValue("@MFeePercentFrom", _CategoryScheme.MFeePercentFrom);
                                cmd.Parameters.AddWithValue("@MFeePercentTo", _CategoryScheme.MFeePercentTo);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _CategoryScheme.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update CategoryScheme set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where CategorySchemePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _CategoryScheme.Notes);
                                cmd.Parameters.AddWithValue("@PK", _CategoryScheme.CategorySchemePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _CategoryScheme.HistoryPK);
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

        public void CategoryScheme_Approved(CategoryScheme _CategoryScheme)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CategoryScheme set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where CategorySchemePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CategoryScheme.CategorySchemePK);
                        cmd.Parameters.AddWithValue("@historyPK", _CategoryScheme.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _CategoryScheme.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CategoryScheme set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CategorySchemePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _CategoryScheme.CategorySchemePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CategoryScheme.ApprovedUsersID);
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

        public void CategoryScheme_Reject(CategoryScheme _CategoryScheme)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CategoryScheme set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where CategorySchemePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CategoryScheme.CategorySchemePK);
                        cmd.Parameters.AddWithValue("@historyPK", _CategoryScheme.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CategoryScheme.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CategoryScheme set status= 2,LastUpdate=@LastUpdate where CategorySchemePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _CategoryScheme.CategorySchemePK);
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

        public void CategoryScheme_Void(CategoryScheme _CategoryScheme)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CategoryScheme set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where CategorySchemePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CategoryScheme.CategorySchemePK);
                        cmd.Parameters.AddWithValue("@historyPK", _CategoryScheme.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CategoryScheme.VoidUsersID);
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


        public List<CategorySchemeCombo> CategoryScheme_Combo()
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CategorySchemeCombo> L_CategoryScheme = new List<CategorySchemeCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "SELECT  CategorySchemePK,ID + ' - ' + Name ID, Name FROM [CategoryScheme]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CategorySchemeCombo M_CategoryScheme = new CategorySchemeCombo();
                                    M_CategoryScheme.CategorySchemePK = Convert.ToInt32(dr["CategorySchemePK"]);
                                    M_CategoryScheme.ID = Convert.ToString(dr["ID"]);
                                    M_CategoryScheme.Name = Convert.ToString(dr["Name"]);
                                    L_CategoryScheme.Add(M_CategoryScheme);
                                }

                            }
                        }
                        return L_CategoryScheme;
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