using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class AdvisoryFeeTOPReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[AdvisoryFeeTOP] " +
                            "([HistoryPK],[Status],[AdvisoryFeePK],[TOPPercent],[TOPDate],[LastUsersID],[LastUpdate])";


        string _paramaterCommand = "@AdvisoryFeePK,@TOPPercent,@TOPDate,@LastUsersID,@LastUpdate";

        //2
        private AdvisoryFeeTOP setAdvisoryFeeTOP(SqlDataReader dr)
        {
            AdvisoryFeeTOP M_AdvisoryFeeTOP = new AdvisoryFeeTOP();
            M_AdvisoryFeeTOP.AdvisoryFeeTOPPK = Convert.ToInt32(dr["AdvisoryFeeTOPPK"]);
            M_AdvisoryFeeTOP.AdvisoryFeePK = Convert.ToInt32(dr["AdvisoryFeePK"]);
            M_AdvisoryFeeTOP.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_AdvisoryFeeTOP.Status = Convert.ToInt32(dr["Status"]);
            M_AdvisoryFeeTOP.TOPPercent = Convert.ToDecimal(dr["TOPPercent"]);
            M_AdvisoryFeeTOP.TOPDate = Convert.ToDateTime(dr["TOPDate"]);
            M_AdvisoryFeeTOP.LastUsersID = Convert.ToString(dr["LastUsersID"]);
            M_AdvisoryFeeTOP.LastUpdate = dr["LastUpdate"].ToString();
            M_AdvisoryFeeTOP.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_AdvisoryFeeTOP;
        }

        //3
        public List<AdvisoryFeeTOP> AdvisoryFeeTOP_Select(int _status, int _AdvisoryFeePK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AdvisoryFeeTOP> L_AdvisoryFeeTOP = new List<AdvisoryFeeTOP>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @" select * from AdvisoryFeeTOP 
                            where AdvisoryFeePK = @AdvisoryFeePK and status = 2
                            order by AdvisoryFeeTOPPK Asc ";
                            cmd.Parameters.AddWithValue("@AdvisoryFeePK", _AdvisoryFeePK);
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select * from AdvisoryFeeTOP where AdvisoryFeePK = @AdvisoryFeePK order by AdvisoryFeeTOPPK Asc ";
                            cmd.Parameters.AddWithValue("@AdvisoryFeePK", _AdvisoryFeePK);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_AdvisoryFeeTOP.Add(setAdvisoryFeeTOP(dr));
                                }
                            }
                            return L_AdvisoryFeeTOP;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //4
        public int AdvisoryFeeTOP_Add(AdvisoryFeeTOP _AdvisoryFeeTOP)
        {
            try
            {
                int _AdvisoryFeeTOPPK = 0;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        _AdvisoryFeeTOPPK = _host.Get_DetailNewAdvisoryFeeTOPPK(_AdvisoryFeeTOP.AdvisoryFeePK, "AdvisoryFeeTOP", "AdvisoryFeePK");
                        cmd.CommandText =
                                  "update AdvisoryFee set lastupdate = @Lastupdate where AdvisoryFeePK = @AdvisoryFeePK and status = 1 \n " +
                                  "update AdvisoryFee set lastupdate = @Lastupdate, status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate where AdvisoryFeePK = @AdvisoryFeePK and status = 2 \n " +
                                    _insertCommand +
                                 " Select 1,2," + _paramaterCommand;
                        cmd.Parameters.AddWithValue("@AdvisoryFeePK", _AdvisoryFeeTOP.AdvisoryFeePK);
                        cmd.Parameters.AddWithValue("@status", _AdvisoryFeeTOP.Status);
                        cmd.Parameters.AddWithValue("@AdvisoryFeeTOPPK", _AdvisoryFeeTOPPK);
                        cmd.Parameters.AddWithValue("@TOPPercent", _AdvisoryFeeTOP.TOPPercent);
                        cmd.Parameters.AddWithValue("@TOPDate", _AdvisoryFeeTOP.TOPDate);
                        cmd.Parameters.AddWithValue("@LastUsersID", _AdvisoryFeeTOP.LastUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                        return _AdvisoryFeeTOPPK;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //Update
        public void AdvisoryFeeTOP_Update(AdvisoryFeeTOP _AdvisoryFeeTOP)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "update AdvisoryFee set status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate, lastupdate = @Lastupdate where AdvisoryFeePK = @AdvisoryFeePK and status = 2 \n " +
                         "Update AdvisoryFeeTOP " +
                        "Set LastUsersID = @LastUsersID,LastUpdate=@lastupdate " +
                        "Where AdvisoryFeePK = @AdvisoryFeePK and AdvisoryFeeTOPPK = @AdvisoryFeeTOPPK";

                        cmd.Parameters.AddWithValue("@TOPPercent", _AdvisoryFeeTOP.TOPPercent);
                        cmd.Parameters.AddWithValue("@TOPDate", _AdvisoryFeeTOP.TOPDate);
                        cmd.Parameters.AddWithValue("@AdvisoryFeePK", _AdvisoryFeeTOP.AdvisoryFeePK);
                        cmd.Parameters.AddWithValue("@AdvisoryFeeTOPPK", _AdvisoryFeeTOP.AdvisoryFeeTOPPK);
                        cmd.Parameters.AddWithValue("@LastUsersID", _AdvisoryFeeTOP.LastUsersID);
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

        public void AdvisoryFeeTOP_Delete(AdvisoryFeeTOP _AdvisoryFeeTOP)
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
                            @"update AdvisoryFee set  lastupdate = @Lastupdate where AdvisoryFeePK = @AdvisoryFeePK and status = 1 
                            update AdvisoryFee set lastupdate = @Lastupdate, status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate where AdvisoryFeePK = @AdvisoryFeePK and status = 2 
                            delete AdvisoryFeeTOP where AdvisoryFeePK = @AdvisoryFeePK and AdvisoryFeeTOPPK = @AdvisoryFeeTOPPK ";
                        cmd.Parameters.AddWithValue("@AdvisoryFeePK", _AdvisoryFeeTOP.AdvisoryFeePK);
                        cmd.Parameters.AddWithValue("@AdvisoryFeeTOPPK", _AdvisoryFeeTOP.AdvisoryFeeTOPPK);
                        cmd.Parameters.AddWithValue("@Lastupdate", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUsersID", _AdvisoryFeeTOP.LastUsersID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public bool CheckAdvisoryFeeTOP(int _pk)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select * from AdvisoryFeeTOP where AdvisoryFeePK = @PK and Status = 1";
                        cmd.Parameters.AddWithValue("@PK", _pk);

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

    }
}
