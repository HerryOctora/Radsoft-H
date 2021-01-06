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
    public class TransactionPromoReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[TransactionPromo] " +
                            "([TransactionPromoPK],[HistoryPK],[Status],[ID],[Name],[Description],";

        string _paramaterCommand = "@ID,@Name,@Description,";

        //2
        private TransactionPromo setTransactionPromo(SqlDataReader dr)
        {
            TransactionPromo M_TransactionPromo = new TransactionPromo();
            M_TransactionPromo.TransactionPromoPK = Convert.ToInt32(dr["TransactionPromoPK"]);
            M_TransactionPromo.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_TransactionPromo.Status = Convert.ToInt32(dr["Status"]);
            M_TransactionPromo.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_TransactionPromo.Notes = Convert.ToString(dr["Notes"]);
            M_TransactionPromo.ID = dr["ID"].ToString();
            M_TransactionPromo.Name = dr["Name"].ToString();
            M_TransactionPromo.Description = dr["Description"].ToString();
            M_TransactionPromo.EntryUsersID = dr["EntryUsersID"].ToString();
            M_TransactionPromo.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_TransactionPromo.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_TransactionPromo.VoidUsersID = dr["VoidUsersID"].ToString();
            M_TransactionPromo.EntryTime = dr["EntryTime"].ToString();
            M_TransactionPromo.UpdateTime = dr["UpdateTime"].ToString();
            M_TransactionPromo.ApprovedTime = dr["ApprovedTime"].ToString();
            M_TransactionPromo.VoidTime = dr["VoidTime"].ToString();
            M_TransactionPromo.DBUserID = dr["DBUserID"].ToString();
            M_TransactionPromo.DBTerminalID = dr["DBTerminalID"].ToString();
            M_TransactionPromo.LastUpdate = dr["LastUpdate"].ToString();
            M_TransactionPromo.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_TransactionPromo;
        }

        public List<TransactionPromo> TransactionPromo_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TransactionPromo> L_TransactionPromo = new List<TransactionPromo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"

                              Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, * from TransactionPromo A
                             where A.status = @status 
                                                     
                               ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"
                             Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, * from TransactionPromo A
                        ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_TransactionPromo.Add(setTransactionPromo(dr));
                                }
                            }
                            return L_TransactionPromo;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int TransactionPromo_Add(TransactionPromo _TransactionPromo, bool _havePrivillege)
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
                                 "Select isnull(max(TransactionPromoPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from TransactionPromo";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _TransactionPromo.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(TransactionPromoPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from TransactionPromo";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _TransactionPromo.ID);
                        cmd.Parameters.AddWithValue("@Name", _TransactionPromo.Name);
                        cmd.Parameters.AddWithValue("@Description", _TransactionPromo.Description);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _TransactionPromo.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_datetimeNow, "TransactionPromo");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int TransactionPromo_Update(TransactionPromo _TransactionPromo, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_TransactionPromo.TransactionPromoPK, _TransactionPromo.HistoryPK, "TransactionPromo");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update TransactionPromo set status=2,Notes=@Notes,ID=@ID,Name=@Name,Description=@Description," +
                                "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where TransactionPromoPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _TransactionPromo.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _TransactionPromo.TransactionPromoPK);
                            cmd.Parameters.AddWithValue("@Notes", _TransactionPromo.Notes);
                            cmd.Parameters.AddWithValue("@ID", _TransactionPromo.ID);
                            cmd.Parameters.AddWithValue("@Name", _TransactionPromo.Name);
                            cmd.Parameters.AddWithValue("@Description", _TransactionPromo.Description);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _TransactionPromo.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _TransactionPromo.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update TransactionPromo set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where TransactionPromoPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _TransactionPromo.TransactionPromoPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _TransactionPromo.EntryUsersID);
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
                                cmd.CommandText = "Update TransactionPromo set Notes=@Notes,ID=@ID,Name=@Name,Description=@Description," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where TransactionPromoPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _TransactionPromo.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _TransactionPromo.TransactionPromoPK);
                                cmd.Parameters.AddWithValue("@Notes", _TransactionPromo.Notes);
                                cmd.Parameters.AddWithValue("@ID", _TransactionPromo.ID);
                                cmd.Parameters.AddWithValue("@Name", _TransactionPromo.Name);
                                cmd.Parameters.AddWithValue("@Description", _TransactionPromo.Description);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _TransactionPromo.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_TransactionPromo.TransactionPromoPK, "TransactionPromo");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From TransactionPromo where TransactionPromoPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _TransactionPromo.TransactionPromoPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _TransactionPromo.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _TransactionPromo.ID);
                                cmd.Parameters.AddWithValue("@Name", _TransactionPromo.Name);
                                cmd.Parameters.AddWithValue("@Description", _TransactionPromo.Description);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _TransactionPromo.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update TransactionPromo set status= 4,Notes=@Notes, " +
                                " LastUpdate=@lastupdate where TransactionPromoPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _TransactionPromo.Notes);
                                cmd.Parameters.AddWithValue("@PK", _TransactionPromo.TransactionPromoPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _TransactionPromo.HistoryPK);
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

        public void TransactionPromo_Approved(TransactionPromo _TransactionPromo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TransactionPromo set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where TransactionPromoPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TransactionPromo.TransactionPromoPK);
                        cmd.Parameters.AddWithValue("@historyPK", _TransactionPromo.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _TransactionPromo.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update TransactionPromo set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where TransactionPromoPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _TransactionPromo.TransactionPromoPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TransactionPromo.ApprovedUsersID);
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

        public void TransactionPromo_Reject(TransactionPromo _TransactionPromo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TransactionPromo set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where TransactionPromoPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TransactionPromo.TransactionPromoPK);
                        cmd.Parameters.AddWithValue("@historyPK", _TransactionPromo.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TransactionPromo.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update TransactionPromo set status= 2,lastupdate=@lastupdate where TransactionPromoPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _TransactionPromo.TransactionPromoPK);
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

        public void TransactionPromo_Void(TransactionPromo _TransactionPromo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TransactionPromo set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate  " +
                            "where TransactionPromoPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TransactionPromo.TransactionPromoPK);
                        cmd.Parameters.AddWithValue("@historyPK", _TransactionPromo.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TransactionPromo.VoidUsersID);
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

        public List<TransactionPromoCombo> TransactionPromo_Combo()
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TransactionPromoCombo> L_TransactionPromo = new List<TransactionPromoCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "SELECT  TransactionPromoPK,ID + ' - ' + Name ID, Name FROM [TransactionPromo]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    TransactionPromoCombo M_TransactionPromo = new TransactionPromoCombo();
                                    M_TransactionPromo.TransactionPromoPK = Convert.ToInt32(dr["TransactionPromoPK"]);
                                    M_TransactionPromo.ID = Convert.ToString(dr["ID"]);
                                    M_TransactionPromo.Name = Convert.ToString(dr["Name"]);
                                    L_TransactionPromo.Add(M_TransactionPromo);
                                }

                            }
                        }
                        return L_TransactionPromo;
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