using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class InstrumentCompanyTypeReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[InstrumentCompanyType] " +
                            "([InstrumentCompanyTypePK],[HistoryPK],[Status],[ID],[Name],[Client],";
        string _paramaterCommand = "@ID,@Name,@Client,";



        //2
        private InstrumentCompanyType setInstrumentCompanyType(SqlDataReader dr)
        {
            InstrumentCompanyType M_InstrumentCompanyType = new InstrumentCompanyType();
            M_InstrumentCompanyType.InstrumentCompanyTypePK = Convert.ToInt32(dr["InstrumentCompanyTypePK"]);
            M_InstrumentCompanyType.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_InstrumentCompanyType.Status = Convert.ToInt32(dr["Status"]);
            M_InstrumentCompanyType.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_InstrumentCompanyType.Notes = Convert.ToString(dr["Notes"]);
            M_InstrumentCompanyType.ID = dr["ID"].ToString();
            M_InstrumentCompanyType.Name = dr["Name"].ToString();
            M_InstrumentCompanyType.Client = dr["Client"].ToString();
            M_InstrumentCompanyType.EntryUsersID = dr["EntryUsersID"].ToString();
            M_InstrumentCompanyType.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_InstrumentCompanyType.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_InstrumentCompanyType.VoidUsersID = dr["VoidUsersID"].ToString();
            M_InstrumentCompanyType.EntryTime = dr["EntryTime"].ToString();
            M_InstrumentCompanyType.UpdateTime = dr["UpdateTime"].ToString();
            M_InstrumentCompanyType.ApprovedTime = dr["ApprovedTime"].ToString();
            M_InstrumentCompanyType.VoidTime = dr["VoidTime"].ToString();
            M_InstrumentCompanyType.DBUserID = dr["DBUserID"].ToString();
            M_InstrumentCompanyType.DBTerminalID = dr["DBTerminalID"].ToString();
            M_InstrumentCompanyType.LastUpdate = dr["LastUpdate"].ToString();
            M_InstrumentCompanyType.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_InstrumentCompanyType;
        }


        public List<InstrumentCompanyType> InstrumentCompanyType_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentCompanyType> L_InstrumentCompanyType = new List<InstrumentCompanyType>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from InstrumentCompanyType where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from InstrumentCompanyType order by ID";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_InstrumentCompanyType.Add(setInstrumentCompanyType(dr));
                                }
                            }
                            return L_InstrumentCompanyType;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int InstrumentCompanyType_Add(InstrumentCompanyType _InstrumentCompanyType, bool _havePrivillege)
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
                                 "Select isnull(max(InstrumentCompanyTypePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from InstrumentCompanyType";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _InstrumentCompanyType.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(InstrumentCompanyTypePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from InstrumentCompanyType";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        //cmd.Parameters.AddWithValue("@Notes", _InstrumentCompanyType.Notes);
                        cmd.Parameters.AddWithValue("@ID", _InstrumentCompanyType.ID);
                        cmd.Parameters.AddWithValue("@Name", _InstrumentCompanyType.Name);
                        cmd.Parameters.AddWithValue("@Client", _InstrumentCompanyType.Client);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _InstrumentCompanyType.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "InstrumentCompanyType");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int InstrumentCompanyType_Update(InstrumentCompanyType _InstrumentCompanyType, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_InstrumentCompanyType.InstrumentCompanyTypePK, _InstrumentCompanyType.HistoryPK, "InstrumentCompanyType");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update InstrumentCompanyType set status=2, Notes=@Notes,ID=@ID,Name=@Name,Client=@Cliet," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where InstrumentCompanyTypePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _InstrumentCompanyType.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _InstrumentCompanyType.InstrumentCompanyTypePK);
                            cmd.Parameters.AddWithValue("@Notes", _InstrumentCompanyType.Notes);
                            cmd.Parameters.AddWithValue("@ID", _InstrumentCompanyType.ID);
                            cmd.Parameters.AddWithValue("@Name", _InstrumentCompanyType.Name);
                            cmd.Parameters.AddWithValue("@Client", _InstrumentCompanyType.Client);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _InstrumentCompanyType.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _InstrumentCompanyType.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update InstrumentCompanyType set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where InstrumentCompanyTypePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _InstrumentCompanyType.InstrumentCompanyTypePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _InstrumentCompanyType.EntryUsersID);
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
                                cmd.CommandText = "Update InstrumentCompanyType set Notes=@Notes,ID=@ID,Name=@Name,Client=@client," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where InstrumentCompanyTypePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _InstrumentCompanyType.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _InstrumentCompanyType.InstrumentCompanyTypePK);
                                cmd.Parameters.AddWithValue("@Notes", _InstrumentCompanyType.Notes);
                                cmd.Parameters.AddWithValue("@ID", _InstrumentCompanyType.ID);
                                cmd.Parameters.AddWithValue("@Name", _InstrumentCompanyType.Name);
                                cmd.Parameters.AddWithValue("@Client", _InstrumentCompanyType.Client);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _InstrumentCompanyType.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_InstrumentCompanyType.InstrumentCompanyTypePK, "InstrumentCompanyType");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From InstrumentCompanyType where InstrumentCompanyTypePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _InstrumentCompanyType.InstrumentCompanyTypePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _InstrumentCompanyType.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _InstrumentCompanyType.ID);
                                cmd.Parameters.AddWithValue("@Name", _InstrumentCompanyType.Name);
                                cmd.Parameters.AddWithValue("@Client", _InstrumentCompanyType.Client);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _InstrumentCompanyType.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update InstrumentCompanyType set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where InstrumentCompanyTypePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _InstrumentCompanyType.Notes);
                                cmd.Parameters.AddWithValue("@PK", _InstrumentCompanyType.InstrumentCompanyTypePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _InstrumentCompanyType.HistoryPK);
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

        public void InstrumentCompanyType_Approved(InstrumentCompanyType _InstrumentCompanyType)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update InstrumentCompanyType set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where InstrumentCompanyTypePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _InstrumentCompanyType.InstrumentCompanyTypePK);
                        cmd.Parameters.AddWithValue("@historyPK", _InstrumentCompanyType.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _InstrumentCompanyType.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update InstrumentCompanyType set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where InstrumentCompanyTypePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _InstrumentCompanyType.InstrumentCompanyTypePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _InstrumentCompanyType.ApprovedUsersID);
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

        public void InstrumentCompanyType_Reject(InstrumentCompanyType _InstrumentCompanyType)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update InstrumentCompanyType set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where InstrumentCompanyTypePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _InstrumentCompanyType.InstrumentCompanyTypePK);
                        cmd.Parameters.AddWithValue("@historyPK", _InstrumentCompanyType.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _InstrumentCompanyType.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update InstrumentCompanyType set status= 2,LastUpdate=@LastUpdate where InstrumentCompanyTypePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _InstrumentCompanyType.InstrumentCompanyTypePK);
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

        public void InstrumentCompanyType_Void(InstrumentCompanyType _InstrumentCompanyType)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update InstrumentCompanyType set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where InstrumentCompanyTypePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _InstrumentCompanyType.InstrumentCompanyTypePK);
                        cmd.Parameters.AddWithValue("@historyPK", _InstrumentCompanyType.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _InstrumentCompanyType.VoidUsersID);
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

        public List<InstrumentCompanyTypeCombo> InstrumentCompanyType_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentCompanyTypeCombo> L_InstrumentCompanyType = new List<InstrumentCompanyTypeCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  InstrumentCompanyTypePK,ID +' - '+ Name ID, Name, Client FROM [InstrumentCompanyType]  where status = 2 order by InstrumentCompanyTypePK";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentCompanyTypeCombo M_InstrumentCompanyType = new InstrumentCompanyTypeCombo();
                                    M_InstrumentCompanyType.InstrumentCompanyTypePK = Convert.ToInt32(dr["InstrumentCompanyTypePK"]);
                                    M_InstrumentCompanyType.ID = Convert.ToString(dr["ID"]);
                                    M_InstrumentCompanyType.Name = Convert.ToString(dr["Name"]);
                                    M_InstrumentCompanyType.Client = Convert.ToString(dr["Client"]);
                                    L_InstrumentCompanyType.Add(M_InstrumentCompanyType);
                                }

                            }
                            return L_InstrumentCompanyType;
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
